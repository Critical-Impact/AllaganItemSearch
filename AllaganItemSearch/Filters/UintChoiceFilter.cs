using System;
using System.Collections.Generic;
using System.Linq;

using AllaganItemSearch.ItemRenderers;

using AllaganLib.GameSheets.Sheets.Rows;
using AllaganLib.Interface.FormFields;
using AllaganLib.Interface.Services;

namespace AllaganItemSearch.Filters;

public class UintChoiceFilter : MultipleChoiceFormField<uint, FilterState>, IItemFilter
{
    private readonly string key;
    private readonly string name;
    private readonly string helpText;
    private readonly Func<ItemRow, uint> transformer;
    private readonly Dictionary<uint, string> choices;

    public delegate UintChoiceFilter Factory(string key, string name, string helpText, Func<ItemRow, uint> transformer, Dictionary<uint, string> choices);

    public UintChoiceFilter(string key, string name, string helpText, Func<ItemRow, uint> transformer, Dictionary<uint, string> choices, ImGuiService imGuiService)
        : base(imGuiService)
    {
        this.key = key;
        this.name = name;
        this.helpText = helpText;
        this.transformer = transformer;
        this.choices = choices;
    }

    public IReadOnlyList<ItemInfoRenderCategory>? Categories => null;

    public RendererType? RendererType => null;

    public override List<uint> DefaultValue { get; set; } = [];

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

    public Func<ItemRow, uint> Transformer => this.transformer;

    public override string Version => "1.0";

    public override bool ShowAddAll => false;

    public override bool ShowClear => true;

    public override bool HideAlreadyPicked { get; set; } = false;

    public bool Match(FilterState filterState, ItemRow row)
    {
        var itemRowValue = this.Transformer.Invoke(row);
        var currentValue = this.CurrentValue(filterState);
        return currentValue.Any(c => c == itemRowValue);
    }

    public override Dictionary<uint, string> GetChoices(FilterState configuration)
    {
        return this.choices;
    }

    public IEnumerable<ItemRow> Match(FilterState filterState, IEnumerable<ItemRow> itemRows)
    {
        var currentValue = this.CurrentValue(filterState).Distinct().ToHashSet();
        if (currentValue.Count == 0)
        {
            return itemRows;
        }
        return itemRows.Where(
            row =>
            {
                var itemRowValue = this.Transformer.Invoke(row);
                return currentValue.Contains(itemRowValue);
            });
    }
}
