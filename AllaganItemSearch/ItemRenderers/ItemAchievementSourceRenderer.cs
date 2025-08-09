using System;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;

using Dalamud.Bindings.ImGui;

namespace AllaganItemSearch.ItemRenderers;

public class ItemAchievementSourceRenderer : ItemInfoRenderer<ItemAchievementSource>
{
    public override RendererType RendererType => RendererType.Source;
    public override ItemInfoType Type => ItemInfoType.Achievement;
    public override string SingularName => "Achievement";

    public override string? PluralName => "Achievements";
    public override string HelpText => "Can the item be earned via an achievement?";
    public override bool ShouldGroup => true;

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var achievementSource = this.AsSource(source);
        ImGui.Text(
            $"{achievementSource.Achievement.Value.Name.ExtractText()} ({achievementSource.Achievement.Value.AchievementCategory.Value.Name.ExtractText()})");
    };

    public override Func<ItemSource, string> GetName => source =>
    {
        var achievementSource = this.AsSource(source);
        return achievementSource.Achievement.Value.Name.ExtractText();
    };

    public override Func<ItemSource, int> GetIcon => source =>
    {
        return Icons.AchievementCertIcon;
    };
}
