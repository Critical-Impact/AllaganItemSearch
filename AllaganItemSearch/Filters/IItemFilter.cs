using System.Collections.Generic;

using AllaganItemSearch.ItemRenderers;

using AllaganLib.GameSheets.Sheets.Rows;
using AllaganLib.Interface.FormFields;

namespace AllaganItemSearch.Filters;

public interface IItemFilter : IFormField<FilterState>
{
    IReadOnlyList<ItemInfoRenderCategory>? Categories { get; }
    RendererType? RendererType { get; }
    public bool Match(FilterState filterState, ItemRow row);

    public IEnumerable<ItemRow> Match(FilterState filterState, IEnumerable<ItemRow> itemRows);
}
