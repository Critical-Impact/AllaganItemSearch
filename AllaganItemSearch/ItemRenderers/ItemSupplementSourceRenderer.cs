using System;
using System.Collections.Generic;
using System.Numerics;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;

using Dalamud.Interface.Textures;
using Dalamud.Plugin.Services;

using ImGuiNET;

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
            ImGui.Image(this.TextureProvider.GetFromGameIcon(new(source.Item.Icon)).GetWrapOrEmpty().ImGuiHandle, new Vector2(18,18) * ImGui.GetIO().FontGlobalScale);
            ImGui.SameLine();
            ImGui.Text(source.Item.NameString);
        }
    };

    public override Action<ItemSource> DrawTooltip => source =>
    {
        ImGui.Image(this.TextureProvider.GetFromGameIcon(new(source.Item.Icon)).GetWrapOrEmpty().ImGuiHandle, new Vector2(18,18) * ImGui.GetIO().FontGlobalScale);
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
            ImGui.Image(this.TextureProvider.GetFromGameIcon(new GameIconLookup(source.CostItem!.Icon)).GetWrapOrEmpty().ImGuiHandle, new Vector2(18,18) * ImGui.GetIO().FontGlobalScale);
            ImGui.SameLine();
            ImGui.Text(source.CostItem!.NameString);
        }
    };

    public override Action<ItemSource> DrawTooltip => source =>
    {
        ImGui.Image(this.TextureProvider.GetFromGameIcon(new GameIconLookup(source.CostItem!.Icon)).GetWrapOrEmpty().ImGuiHandle, new Vector2(18,18) * ImGui.GetIO().FontGlobalScale);
        ImGui.SameLine();
        ImGui.Text(source.CostItem!.NameString);
    };

    public override Func<ItemSource, int> GetIcon => _ => this.icon;

    public override Func<ItemSource, string> GetName => _ => "";
}
