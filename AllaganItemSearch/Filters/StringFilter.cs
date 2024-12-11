using System;
using System.Collections.Generic;
using System.Linq;

using AllaganLib.GameSheets.Sheets.Rows;
using AllaganLib.Interface.FormFields;
using AllaganLib.Interface.Services;
using AllaganLib.Shared.Extensions;

namespace AllaganItemSearch.Filters;

public class StringFilter : StringFormField<FilterState>, IItemFilter
{
    private readonly string key;
    private readonly string name;
    private readonly string helpText;
    private readonly Func<ItemRow, string> transformer;

    public delegate StringFilter Factory(string key, string name, string helpText, Func<ItemRow, string> transformer);

    public StringFilter(string key, string name, string helpText, Func<ItemRow, string> transformer, ImGuiService imGuiService)
        : base(imGuiService)
    {
        this.key = key;
        this.name = name;
        this.helpText = helpText;
        this.transformer = transformer;
    }

    public override string DefaultValue { get; set; } = string.Empty;

    public override string Key
    {
        get => this.key;
        set { }
    }

    public override string Name
    {
        get => this.name;
        set { }
    }

    public override string HelpText
    {
        get => this.helpText;
        set { }
    }

    public override string Version => "1.0";

    public Func<ItemRow, string> Transformer => this.transformer;

    public bool Match(FilterState filterState, ItemRow row)
    {
        var asString = this.Transformer.Invoke(row).ToLower();
        var currentValue = new FilterComparisonText(this.CurrentValue(filterState).ToLower());
        return asString.PassesFilter(currentValue);
    }

    public IEnumerable<ItemRow> Match(FilterState filterState, IEnumerable<ItemRow> itemRows)
    {
        var currentValue = new FilterComparisonText(this.CurrentValue(filterState).ToLower());
        return itemRows.Where(c => this.Transformer.Invoke(c).ToLower().PassesFilter(currentValue));
    }
}
