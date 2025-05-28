using System;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;

using ImGuiNET;

namespace AllaganItemSearch.ItemRenderers;

public class ItemBattleLeveSourceRenderer : ItemInfoRenderer<ItemBattleLeveSource>
{
    public override RendererType RendererType { get; } = RendererType.Source;

    public override ItemInfoType Type { get; } = ItemInfoType.BattleLeve;

    public override string SingularName => "Battle Leve";

    public override string PluralName => "Battle Leves";

    public override string HelpText => "Is this item obtained from a battle leve?";

    public override bool ShouldGroup => true;

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = this.AsSource(source);
        var leveRow = asSource.Leve.Value;
        ImGui.TextUnformatted("Leve: " + leveRow.Name.ExtractText());
        ImGui.TextUnformatted("Class: " + leveRow.ClassJobCategory.Value.Name.ExtractText());
        ImGui.TextUnformatted("EXP Reward: " + asSource.ExpReward);
        ImGui.TextUnformatted("Allowance Cost: " + leveRow.AllowanceCost);
        ImGui.TextUnformatted("Loot Chance: " + asSource.LeveRewardItem.Value.ProbabilityPercent[asSource.RewardItemIndex] + "%");
    };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = this.AsSource(source);
        var leveRow = asSource.Leve.Value;
        return leveRow.Name.ExtractText();
    };

    public override Func<ItemSource, int> GetIcon => _ => Icons.LeveIcon;
}
