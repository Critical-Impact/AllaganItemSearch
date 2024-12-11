using System;
using System.Collections.Generic;
using System.Linq;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;

using ImGuiNET;

namespace AllaganItemSearch.ItemRenderers;

public class ItemAirshipDropSourceRenderer : ItemInfoRenderer<ItemAirshipDropSource>
{
    public override RendererType RendererType => RendererType.Source;
    public override ItemInfoType Type => ItemInfoType.Airship;
    public override string SingularName => "Airship Exploration";
    public override bool ShouldGroup => true;
    public override string HelpText => "Can the item be earned from a airship exploration route?";
    
    public override Action<List<ItemSource>>? DrawTooltipGrouped => sources =>
    {
        var airshipDropSources = this.AsSource(sources).DistinctBy(c => c.AirshipExplorationPoint.RowId);
        foreach (var source in airshipDropSources)
        {
            ImGui.Text(
                $"{source.AirshipExplorationPoint.Base.Name.ExtractText()}");
        }
    };

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var airshipDropSource = this.AsSource(source);
        ImGui.Text(
            $"{airshipDropSource.AirshipExplorationPoint.Base.Name.ExtractText()}");
    };
    public override Func<ItemSource, string> GetName => source =>
    {
        var airshipDropSource = this.AsSource(source);
        return airshipDropSource.AirshipExplorationPoint.Base.Name.ExtractText();
    };

    public override Func<ItemSource, int> GetIcon => source =>
    {
        return Icons.AirshipIcon;
    };
}
