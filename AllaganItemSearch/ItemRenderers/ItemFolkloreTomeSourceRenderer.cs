using System;
using System.Collections.Generic;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;
using AllaganLib.Shared.Extensions;

using Dalamud.Bindings.ImGui;

namespace AllaganItemSearch.ItemRenderers;

public class ItemFolkloreTomeSourceRenderer : ItemInfoRenderer<ItemFolkloreTomeSource>
{
    public override IReadOnlyList<ItemInfoRenderCategory> Categories =>
        [ItemInfoRenderCategory.Gathering, ItemInfoRenderCategory.HiddenGathering];

    public override RendererType RendererType => RendererType.Use;

    public override ItemInfoType Type => ItemInfoType.FolkloreTome;

    public override string SingularName => "Folklore Tome";

    public override string? PluralName => "Folklore Tomes";

    public override string HelpText => "Does this item unlock additional gathering items when read?";

    public override bool ShouldGroup => true;

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = this.AsSource(source);
        var divisionName = asSource.NotebookDivision.ValueNullable?.Name.ToImGuiString();
        if (!string.IsNullOrEmpty(divisionName))
        {
            ImGui.Text("Unlocks: " + divisionName);
        }
    };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = this.AsSource(source);
        return asSource.NotebookDivision.ValueNullable?.Name.ToImGuiString()
               ?? asSource.Item.NameString;
    };

    public override Func<ItemSource, int> GetIcon => source =>
    {
        var asSource = this.AsSource(source);
        return asSource.Item.Base.Icon;
    };
}
