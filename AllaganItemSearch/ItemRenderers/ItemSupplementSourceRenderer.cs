using System;
using System.Collections.Generic;
using System.Numerics;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;
using AllaganLib.GameSheets.Sheets;

using Dalamud.Interface.Textures;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

using Dalamud.Bindings.ImGui;

namespace AllaganItemSearch.ItemRenderers;

public class ItemDesynthSourceRenderer : ItemSupplementSourceRenderer<ItemDesynthSource>
{
    public ItemDesynthSourceRenderer(ITextureProvider iTextureProvider) : base(iTextureProvider, ItemInfoType.Desynthesis, Icons.DesynthesisIcon)
    {
    }

    public override string SingularName => "Desynthesis";
    public override string HelpText => "Can the item be obtained via desynthesis?";
}

public class ItemReductionSourceRenderer : ItemSupplementSourceRenderer<ItemReductionSource>
{
    public ItemReductionSourceRenderer(ITextureProvider iTextureProvider) : base(iTextureProvider, ItemInfoType.Reduction, Icons.ReductionIcon)
    {
    }

    public override string SingularName => "Reduction";
    public override string HelpText => "Can the item be obtained via reduction?";
}

public class ItemLootSourceRenderer : ItemSupplementSourceRenderer<ItemLootSource>
{
    public ItemLootSourceRenderer(ITextureProvider iTextureProvider) : base(iTextureProvider, ItemInfoType.Loot, Icons.LootIcon)
    {
    }

    public override string SingularName => "Loot";
    public override string HelpText => "Can the item be obtained from another item(normally a chest/material container/coffer)?";
}

public class ItemGardeningSourceRenderer : ItemSupplementSourceRenderer<ItemGardeningSource>
{
    public ItemGardeningSourceRenderer(ITextureProvider iTextureProvider) : base(iTextureProvider, ItemInfoType.Gardening, Icons.SproutIcon)
    {
    }

    public override string SingularName => "Gardening";
    public override string HelpText => "Can the item be grown via gardening?";
}
public class ItemDesynthUseRenderer : ItemSupplementUseRenderer<ItemDesynthSource>
{
    public ItemDesynthUseRenderer(ITextureProvider iTextureProvider) : base(iTextureProvider, ItemInfoType.Desynthesis, Icons.DesynthesisIcon)
    {
    }

    public override string SingularName => "Desynthesis";
    public override string HelpText => "Can the item be desynthesized?";
}

public class ItemReductionUseRenderer : ItemSupplementUseRenderer<ItemReductionSource>
{
    public ItemReductionUseRenderer(ITextureProvider iTextureProvider) : base(iTextureProvider, ItemInfoType.Reduction, Icons.ReductionIcon)
    {
    }

    public override string SingularName => "Reduction";
    public override string HelpText => "Can the item be reduced?";
}

public class ItemLootUseRenderer : ItemSupplementUseRenderer<ItemLootSource>
{
    public ItemLootUseRenderer(ITextureProvider iTextureProvider) : base(iTextureProvider, ItemInfoType.Loot, Icons.LootIcon)
    {
    }

    public override string SingularName => "Loot";
    public override string HelpText => "Does this item contain other items?";
}

public class ItemGardeningUseRenderer : ItemSupplementUseRenderer<ItemGardeningSource>
{
    public ItemGardeningUseRenderer(ITextureProvider iTextureProvider) : base(iTextureProvider, ItemInfoType.Gardening, Icons.SproutIcon)
    {
    }

    public override string SingularName => "Gardening";
    public override string HelpText => "Can the item be used for gardening?";
}

public class ItemCardPackSourceRenderer : ItemSupplementSourceRenderer<ItemCardPackSource>
{
    public ItemCardPackSourceRenderer(ITextureProvider textureProvider) : base(textureProvider,  ItemInfoType.CardPack, Icons.CardPackIcon)
    {
    }

    public override string SingularName => "Card Pack";
    public override string HelpText => "Can the item be obtained from a card pack?";
}

public class ItemCardPackUseRenderer : ItemSupplementUseRenderer<ItemCardPackSource>
{
    public ItemCardPackUseRenderer(ITextureProvider textureProvider) : base(textureProvider, ItemInfoType.CardPack, Icons.CardPackIcon)
    {
    }

    public override string SingularName => "Card Pack";
    public override string HelpText => "Does this item contain cards?";
}

public class ItemCofferSourceRenderer : ItemSupplementSourceRenderer<ItemCofferSource>
{
    public ItemCofferSourceRenderer(ITextureProvider textureProvider) : base(textureProvider,  ItemInfoType.Coffer, Icons.CofferIcon)
    {
    }

    public override string SingularName => "Coffer";
    public override string HelpText => "Can the item be obtained from a coffer?";
}

public class ItemCofferUseRenderer : ItemSupplementUseRenderer<ItemCofferSource>
{
    public ItemCofferUseRenderer(ITextureProvider textureProvider) : base(textureProvider, ItemInfoType.Coffer, Icons.CofferIcon)
    {
    }

    public override string SingularName => "Coffer";
    public override string HelpText => "Is this an item coffer that contains other items?";
}


public class ItemPalaceOfTheDeadSourceRenderer : ItemSupplementSourceRenderer<ItemPalaceOfTheDeadSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.DeepDungeon];
    public ItemPalaceOfTheDeadSourceRenderer(ITextureProvider textureProvider) : base(textureProvider,  ItemInfoType.PalaceOfTheDead, Icons.DeepDungeonIcon)
    {
    }

    public override string SingularName => "Palace of the Dead";
    public override string HelpText => "Can the item be obtained from a loot item in the Palace of the Dead?";
}

public class ItemPalaceOfTheDeadUseRenderer : ItemSupplementUseRenderer<ItemPalaceOfTheDeadSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.DeepDungeon];
    public ItemPalaceOfTheDeadUseRenderer(ITextureProvider textureProvider) : base(textureProvider, ItemInfoType.PalaceOfTheDead, Icons.DeepDungeonIcon)
    {
    }

    public override string SingularName => "Palace of the Dead";
    public override string HelpText => "Is this a loot item obtained in the Palace of the Dead?";
}
public class ItemHeavenOnHighSourceRenderer : ItemSupplementSourceRenderer<ItemHeavenOnHighSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.DeepDungeon];
    public ItemHeavenOnHighSourceRenderer(ITextureProvider textureProvider) : base(textureProvider,  ItemInfoType.HeavenOnHigh, Icons.DeepDungeonIcon)
    {
    }

    public override string SingularName => "Heaven on High";
    public override string HelpText => "Can the item be obtained from a loot item in the Heaven on High?";
}

public class ItemHeavenOnHighUseRenderer : ItemSupplementUseRenderer<ItemHeavenOnHighSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.DeepDungeon];
    public ItemHeavenOnHighUseRenderer(ITextureProvider textureProvider) : base(textureProvider, ItemInfoType.HeavenOnHigh, Icons.DeepDungeonIcon)
    {
    }

    public override string SingularName => "Heaven on High";
    public override string HelpText => "Is this a loot item obtained in the Heaven on High?";
}
public class ItemEurekaOrthosSourceRenderer : ItemSupplementSourceRenderer<ItemEurekaOrthosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation];
    public ItemEurekaOrthosSourceRenderer(ITextureProvider textureProvider) : base(textureProvider,  ItemInfoType.EurekaOrthos, Icons.DeepDungeonIcon)
    {
    }

    public override string SingularName => "Eureka Orthos";
    public override string HelpText => "Can the item be obtained from a loot item in the Eureka Orthos?";
}

public class ItemEurekaOrthosUseRenderer : ItemSupplementUseRenderer<ItemEurekaOrthosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation];
    public ItemEurekaOrthosUseRenderer(ITextureProvider textureProvider) : base(textureProvider, ItemInfoType.EurekaOrthos, Icons.DeepDungeonIcon)
    {
    }

    public override string SingularName => "Eureka Orthos";
    public override string HelpText => "Is this a loot item obtained in the Eureka Orthos?";
}

public class ItemAnemosSourceRenderer : ItemSupplementSourceRenderer<ItemAnemosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation];
    public ItemAnemosSourceRenderer(ITextureProvider textureProvider) : base(textureProvider,  ItemInfoType.Anemos, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "Eureka Anemos";
    public override string HelpText => "Can the item be obtained from a loot item in Eureka Anemos?";
}

public class ItemAnemosUseRenderer : ItemSupplementUseRenderer<ItemAnemosSource>
{
    public ItemAnemosUseRenderer(ITextureProvider textureProvider) : base(textureProvider, ItemInfoType.Anemos, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "Eureka Anemos";
    public override string HelpText => "Is this a loot item obtained in the Eureka Anemos?";
}
public class ItemPagosSourceRenderer : ItemSupplementSourceRenderer<ItemPagosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Pagos];
    public ItemPagosSourceRenderer(ITextureProvider textureProvider) : base(textureProvider,  ItemInfoType.Pagos, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "Eureka Pagos";
    public override string HelpText => "Can the item be obtained from a loot item in Eureka Pagos?";
}

public class ItemPagosUseRenderer : ItemSupplementUseRenderer<ItemPagosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Pagos];
    public ItemPagosUseRenderer(ITextureProvider textureProvider) : base(textureProvider, ItemInfoType.Pagos, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "Eureka Pagos";
    public override string HelpText => "Is this a loot item obtained in the Eureka Pagos?";
}
public class ItemPyrosSourceRenderer : ItemSupplementSourceRenderer<ItemPyrosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Pyros];
    public ItemPyrosSourceRenderer(ITextureProvider textureProvider) : base(textureProvider,  ItemInfoType.Pyros, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "Eureka Pyros";
    public override string HelpText => "Can the item be obtained from a loot item in Eureka Pyros?";
}

public class ItemPyrosUseRenderer : ItemSupplementUseRenderer<ItemPyrosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Pyros];
    public ItemPyrosUseRenderer(ITextureProvider textureProvider) : base(textureProvider, ItemInfoType.Pyros, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "Eureka Pyros";
    public override string HelpText => "Is this a loot item obtained in the Eureka Pyros?";
}

public class ItemHydatosSourceRenderer : ItemSupplementSourceRenderer<ItemHydatosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Hydatos];
    public ItemHydatosSourceRenderer(ITextureProvider textureProvider) : base(textureProvider,  ItemInfoType.Hydatos, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "Eureka Hydatos";
    public override string HelpText => "Can the item be obtained from a loot item in Eureka Hydatos?";
}

public class ItemHydatosUseRenderer : ItemSupplementUseRenderer<ItemHydatosSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory>? Categories { get; } =
        [ItemInfoRenderCategory.FieldOperation, ItemInfoRenderCategory.Hydatos];
    public ItemHydatosUseRenderer(ITextureProvider textureProvider) : base(textureProvider, ItemInfoType.Hydatos, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "Eureka Hydatos";
    public override string HelpText => "Is this a loot item obtained in the Eureka Hydatos?";
}

public class ItemBozjaSourceRenderer : ItemSupplementSourceRenderer<ItemBozjaSource>
{
    public ItemBozjaSourceRenderer(ITextureProvider textureProvider) : base(textureProvider,  ItemInfoType.Bozja, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "Bozja";
    public override string HelpText => "Can the item be obtained from a loot item in Bozja?";
}

public class ItemBozjaUseRenderer : ItemSupplementUseRenderer<ItemBozjaSource>
{
    public ItemBozjaUseRenderer(ITextureProvider textureProvider) : base(textureProvider, ItemInfoType.Bozja, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "Bozja";
    public override string HelpText => "Is this a loot item obtained in the Bozja?";
}
public class ItemLogogramSourceRenderer : ItemSupplementSourceRenderer<ItemLogogramSource>
{
    public ItemLogogramSourceRenderer(ITextureProvider textureProvider) : base(textureProvider,  ItemInfoType.Logogram, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "Logogram";
    public override string HelpText => "Can the item be obtained from a logogram?";
}

public class ItemLogogramUseRenderer : ItemSupplementUseRenderer<ItemLogogramSource>
{
    public ItemLogogramUseRenderer(ITextureProvider textureProvider) : base(textureProvider, ItemInfoType.Logogram, Icons.FieldOpsIcon)
    {
    }

    public override string SingularName => "Logogram";
    public override string HelpText => "Is this item a logogram?";

    public override Func<ItemSource, int> GetIcon => source =>
    {
        return source.CostItem!.Icon;
    };
}



public abstract class ItemSupplementUseRenderer<T> : ItemSupplementSourceRenderer<T> where T : ItemSupplementSource
{
    public override RendererType RendererType => RendererType.Use;

    protected ItemSupplementUseRenderer(ITextureProvider iTextureProvider, ItemInfoType itemInfoType, ushort icon) : base(iTextureProvider, itemInfoType, icon)
    {
    }

    public override Action<List<ItemSource>>? DrawTooltipGrouped => sources =>
    {
        foreach (var source in sources)
        {
            ImGui.Image(this.TextureProvider.GetFromGameIcon(new(source.Item.Icon)).GetWrapOrEmpty().Handle, new Vector2(18,18) * ImGui.GetIO().FontGlobalScale);
            ImGui.SameLine();
            ImGui.Text(source.Item.NameString);
        }
    };

    public override Action<ItemSource> DrawTooltip => source =>
    {
        ImGui.Image(this.TextureProvider.GetFromGameIcon(new(source.Item.Icon)).GetWrapOrEmpty().Handle, new Vector2(18,18) * ImGui.GetIO().FontGlobalScale);
        ImGui.SameLine();
        ImGui.Text(source.Item.NameString);
    };
}

public abstract class ItemSupplementSourceRenderer<T> : ItemInfoRenderer<T> where T : ItemSupplementSource
{
    public ITextureProvider TextureProvider { get; }

    private readonly ItemInfoType itemInfoType;
    private readonly ushort icon;

    public ItemSupplementSourceRenderer(ITextureProvider textureProvider, ItemInfoType itemInfoType, ushort icon)
    {
        this.TextureProvider = textureProvider;
        this.itemInfoType = itemInfoType;
        this.icon = icon;
    }

    public override RendererType RendererType => RendererType.Source;
    public override ItemInfoType Type => this.itemInfoType;
    public override bool ShouldGroup => true;

    public override Action<List<ItemSource>>? DrawTooltipGrouped => sources =>
    {
        foreach (var source in sources)
        {
            ImGui.Image(this.TextureProvider.GetFromGameIcon(new GameIconLookup(source.CostItem!.Icon)).GetWrapOrEmpty().Handle, new Vector2(18,18) * ImGui.GetIO().FontGlobalScale);
            ImGui.SameLine();
            ImGui.Text(source.CostItem!.NameString);
        }
    };

    public override Action<ItemSource> DrawTooltip => source =>
    {
        ImGui.Image(this.TextureProvider.GetFromGameIcon(new GameIconLookup(source.CostItem!.Icon)).GetWrapOrEmpty().Handle, new Vector2(18,18) * ImGui.GetIO().FontGlobalScale);
        ImGui.SameLine();
        ImGui.Text(source.CostItem!.NameString);
    };

    public override Func<ItemSource, int> GetIcon => _ => this.icon;

    public override Func<ItemSource, string> GetName => _ => "";
}
