using System;
using System.Collections.Generic;
using System.Linq;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;

namespace AllaganItemSearch.ItemRenderers;

public class ItemAnimaShopUseRenderer : ItemAnimaShopSourceRenderer
{
    public override RendererType RendererType => RendererType.Use;

    public override string HelpText => "Can the item be spent at an anima shop?";

    public override Func<ItemSource, int> GetIcon => source =>
    {
        var asSource = this.AsSource(source);
        return asSource.Item.Icon;
    };
}

public class ItemAnimaShopSourceRenderer : ItemInfoRenderer<ItemAnimaShopSource>
{
    public override RendererType RendererType => RendererType.Source;

    public override ItemInfoType Type => ItemInfoType.AnimaShop;

    public override string SingularName => "Anima Shop";

    public override string PluralName => "Anima Shops";

    public override string HelpText => "Can the item be purchased from a anima currency shop?";

    public override bool ShouldGroup => true;

    public override byte MaxColumns => 3;

    public override IReadOnlyList<ItemInfoRenderCategory> Categories => [ItemInfoRenderCategory.Shop];

    public override Func<List<ItemSource>, List<List<ItemSource>>>? CustomGroup => sources =>
    {
        return sources.GroupBy(c => (c.CostItems.Count, c.CostItems.FirstOrDefault()!.ItemRow?.RowId ?? null))
                      .Select(c => c.ToList()).ToList();
    };

    public override Action<ItemSource> DrawTooltip => source => { };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = this.AsSource(source);
        return asSource.Shop.Name;
    };

    public override Func<ItemSource, int> GetIcon => source =>
    {
        var asSource = this.AsSource(source);
        return asSource.CostItems.FirstOrDefault()!.ItemRow?.Icon ?? asSource.Item.Icon;
    };
}
