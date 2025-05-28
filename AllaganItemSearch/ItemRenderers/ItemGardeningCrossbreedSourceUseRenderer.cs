using AllaganItemSearch.ItemRenderers;

namespace InventoryTools.Logic.ItemRenderers;

public class ItemGardeningCrossbreedSourceUseRenderer : ItemGardeningCrossbreedSourceRenderer
{
    public override RendererType RendererType => RendererType.Use;
    public override string SingularName => "Gardening Crossbreed Seed";
    public override string HelpText => "Is this item part of a crossbreed when gardening?";
}
