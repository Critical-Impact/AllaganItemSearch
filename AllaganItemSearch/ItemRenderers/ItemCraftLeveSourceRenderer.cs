using System;
using System.Collections.Generic;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;

using Dalamud.Bindings.ImGui;

namespace AllaganItemSearch.ItemRenderers;

public class ItemCraftLeveSourceRenderer : ItemInfoRenderer<ItemCraftLeveSource>
{
    public override RendererType RendererType => RendererType.Use;
    public override ItemInfoType Type => ItemInfoType.CraftLeve;
    public override string SingularName => "Craft Leve";
    public override string PluralName => "Craft Leves";
    public override string HelpText => "Can the item be used in a craft leve?";
    public override bool ShouldGroup => true;
    public override IReadOnlyList<ItemInfoRenderCategory> Categories => [ItemInfoRenderCategory.Leve];
    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = this.AsSource(source);
        var leveRow = asSource.CraftLeve.Value.Leve.Value;
        ImGui.Text("Leve: " + leveRow.Name.ExtractText());
        ImGui.Text("Class: " + leveRow.ClassJobCategory.Value.Name.ExtractText());
        ImGui.Text("EXP Reward: " + leveRow.ExpReward);
        ImGui.Text("Allowance Cost: " + leveRow.AllowanceCost);
    };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = this.AsSource(source);
        var leveRow = asSource.CraftLeve.Value.Leve.Value;
        return leveRow.Name.ExtractText();
    };
    public override Func<ItemSource, int> GetIcon => _ => Icons.LeveIcon;
}
