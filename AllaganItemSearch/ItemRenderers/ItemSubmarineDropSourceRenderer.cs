using System;
using System.Collections.Generic;
using System.Linq;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;

using ImGuiNET;

namespace AllaganItemSearch.ItemRenderers;

public class ItemSubmarineDropSourceRenderer : ItemInfoRenderer<ItemSubmarineDropSource>
{
    public override RendererType RendererType => RendererType.Source;
    public override ItemInfoType Type => ItemInfoType.Submarine;
    public override string SingularName => "Submarine Exploration";
    public override string HelpText => "Can the item be earned from a submarine exploration route?";
    public override bool ShouldGroup => true;
    
    public override Action<List<ItemSource>>? DrawTooltipGrouped => sources =>
    {
        var submarineDropSources = this.AsSource(sources).DistinctBy(c => c.SubmarineExploration.RowId);
        foreach (var source in submarineDropSources)
        {
            ImGui.Text(
                $"{source.SubmarineExploration.Base.Location.ExtractText()}");
        }
    };

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var submarineDropSource = this.AsSource(source);
        ImGui.Text(
            $"{submarineDropSource.SubmarineExploration.Base.Location.ExtractText()}");
    };
    public override Func<ItemSource, string> GetName => source =>
    {
        var submarineDropSource = this.AsSource(source);
        return submarineDropSource.SubmarineExploration.Base.Location.ExtractText();
    };

    public override Func<ItemSource, int> GetIcon => source =>
    {
        return Icons.SubmarineIcon;
    };
}
