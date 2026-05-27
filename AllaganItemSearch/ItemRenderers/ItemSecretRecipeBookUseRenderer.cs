using System;
using System.Collections.Generic;
using System.Linq;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;
using AllaganLib.GameSheets.Sheets;

using Dalamud.Bindings.ImGui;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace AllaganItemSearch.ItemRenderers;

public class ItemSecretRecipeBookUseRenderer : ItemInfoRenderer<ItemSecretRecipeBookUse>
{
    public override RendererType RendererType => RendererType.Use;

    public override ItemInfoType Type => ItemInfoType.SecretRecipeBook;

    public override string SingularName => "Master Recipe Book";

    public override string? PluralName => "Master Recipe Books";

    public override string HelpText => "Is this item used to unlock master recipes?";

    public override bool ShouldGroup => false;

    public override IReadOnlyList<ItemInfoRenderCategory> Categories => [ItemInfoRenderCategory.Crafting];

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = this.AsSource(source);
        var bookName = asSource.SecretRecipeBook.ValueNullable?.Name.ExtractText();
        if (!string.IsNullOrEmpty(bookName))
        {
            ImGui.Text("Unlocks: " + bookName);
        }
    };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = this.AsSource(source);
        return asSource.SecretRecipeBook.ValueNullable?.Name.ExtractText() ?? asSource.Item.NameString;
    };

    public override Func<ItemSource, int> GetIcon => source =>
    {
        var asSource = this.AsSource(source);
        return asSource.Item.Base.Icon;
    };
}
