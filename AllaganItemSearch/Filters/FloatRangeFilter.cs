using System;
using System.Collections.Generic;
using System.Linq;

using AllaganLib.GameSheets.Sheets.Rows;
using AllaganLib.Interface.FormFields;
using AllaganLib.Interface.Services;

namespace AllaganItemSearch.Filters;

public class FloatRangeFilter : FloatRangeFormField<FilterState>, IItemFilter
{
    private readonly string key;
    private readonly string name;
    private readonly string helpText;
    private readonly Func<ItemRow, float> transformer;
    private (float Min, float Max) defaultValue;

    public delegate FloatRangeFilter Factory(string key, string name, string helpText, Func<ItemRow, float> transformer, float minimumValue, float maximumValue);

    public FloatRangeFilter(string key, string name, string helpText, Func<ItemRow, float> transformer, float minimumValue, float maximumValue, ImGuiService imGuiService)
        : base(minimumValue, maximumValue, imGuiService)
    {
        this.key = key;
        this.name = name;
        this.helpText = helpText;
        this.transformer = transformer;
        this.defaultValue = (minimumValue, maximumValue);
    }

    public override (float Min, float Max)? DefaultValue
    {
        get => this.defaultValue;
        set { }
    }

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

    public Func<ItemRow, float> Transformer => this.transformer;

    public bool Match(FilterState filterState, ItemRow row)
    {
        var itemValue = this.Transformer.Invoke(row);
        var currentValue = this.CurrentValue(filterState);
        if (currentValue == null)
        {
            return true;
        }

        return itemValue >= currentValue.Value.Min && itemValue <= currentValue.Value.Max;
    }

    public IEnumerable<ItemRow> Match(FilterState filterState, IEnumerable<ItemRow> itemRows)
    {
        var currentValue = this.CurrentValue(filterState);
        if (currentValue == null)
        {
            return itemRows;
        }

        return itemRows.Where(
            c =>
        {
            var itemValue = this.Transformer.Invoke(c);
            return itemValue >= currentValue.Value.Min && itemValue <= currentValue.Value.Max;
        });
    }
}
