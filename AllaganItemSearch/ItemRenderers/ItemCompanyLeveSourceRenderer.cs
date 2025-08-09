using System;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;

using Dalamud.Bindings.ImGui;

namespace AllaganItemSearch.ItemRenderers;

public class ItemCompanyLeveSourceRenderer : ItemInfoRenderer<ItemCompanyLeveSource>
{
    public override RendererType RendererType { get; } = RendererType.Source;

    public override ItemInfoType Type { get; } = ItemInfoType.CompanyLeve;

    public override string SingularName => "Company Leve";

    public override string PluralName => "Company Leves";

    public override string HelpText => "Is this item obtained from a company leve?";

    public override bool ShouldGroup => true;

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = this.AsSource(source);
        var leveRow = asSource.Leve.Value;
        ImGui.TextUnformatted("Leve: " + leveRow.Name.ExtractText());
        ImGui.TextUnformatted("Class: " + leveRow.ClassJobCategory.Value.Name.ExtractText());
        ImGui.TextUnformatted("EXP Reward: " + asSource.ExpReward);
        ImGui.TextUnformatted("Seals Rewarded: " + asSource.SealsRewarded);
        ImGui.TextUnformatted("Allowance Cost: " + leveRow.AllowanceCost);
    };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = this.AsSource(source);
        var leveRow = asSource.Leve.Value;
        return leveRow.Name.ExtractText();
    };

    public override Func<ItemSource, int> GetIcon => _ => Icons.LeveIcon;
}
