using System;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;

namespace AllaganItemSearch.ItemRenderers;

public class ItemGearsetUseRenderer : ItemInfoRenderer<ItemGearsetSource>
{
    public override RendererType RendererType => RendererType.Use;

    public override ItemInfoType Type => ItemInfoType.Gearset;

    public override string SingularName => "Gearset";

    public override string HelpText => "Is this item part of a gearset?";

    public override bool ShouldGroup => true;

    public override Action<ItemSource> DrawTooltip => source => { };

    public override Func<ItemSource, string> GetName => source => string.Empty;

    public override Func<ItemSource, int> GetIcon => gearset => Icons.ArmorIcon;
}
