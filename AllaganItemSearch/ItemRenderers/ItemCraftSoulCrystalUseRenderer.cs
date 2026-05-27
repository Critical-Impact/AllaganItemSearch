using System;
using System.Collections.Generic;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;

using Dalamud.Bindings.ImGui;

namespace AllaganItemSearch.ItemRenderers;

public class ItemCraftSoulCrystalUseRenderer : ItemInfoRenderer<ItemCraftSoulCrystalUse>
{
    public override RendererType RendererType => RendererType.Use;

    public override ItemInfoType Type => ItemInfoType.CraftSoulCrystal;

    public override string SingularName => "Craft Soul Crystal";

    public override string PluralName => "Craft Soul Crystals";

    public override string HelpText => "Is this soul crystal equipped to specialize in a crafting class?";

    public override bool ShouldGroup => false;

    public override IReadOnlyList<ItemInfoRenderCategory> Categories =>
        [ItemInfoRenderCategory.Crafting];

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = this.AsSource(source);
        ImGui.Text("Equip to specialize as " + asSource.ClassJob.Base.Name.ExtractText());
    };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = this.AsSource(source);
        return asSource.ClassJob.Base.Name.ExtractText();
    };

    public override Func<ItemSource, int> GetIcon => source =>
    {
        var asSource = this.AsSource(source);
        return asSource.ClassJob.Icon;
    };
}
