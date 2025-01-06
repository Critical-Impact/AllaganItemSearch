using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AllaganItemSearch.Filters;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.Model;
using AllaganLib.GameSheets.Service;
using AllaganLib.GameSheets.Sheets;
using AllaganLib.GameSheets.Sheets.Rows;

using Dalamud.Plugin.Services;

using Lumina.Excel.Sheets;

namespace AllaganItemSearch.Services;

public class FilterService : IDisposable
{
    private readonly StringFilter.Factory stringFilterFactory;
    private readonly YesNoChoiceFilter.Factory yesNoChoiceFactory;
    private readonly UintChoiceFilter.Factory uintChoiceFactory;
    private readonly FloatRangeFilter.Factory floatRangeFactory;
    private readonly UintRangeFilter.Factory uintRangeFactory;
    private readonly ItemInfoRenderService itemInfoRenderService;
    private readonly IDataManager dataManager;
    private readonly SheetManager sheetManager;
    private readonly FilterState filterState;
    private readonly ItemInfoCache itemInfoCache;
    private CancellationTokenSource cancellationTokenSource;

    private List<IItemFilter> filters = new();

    private List<ItemRow> itemRows = new();

    private List<ItemRow> defaultItemList = new();

    private Task<List<ItemRow>>? filterTask = null;

    public FilterService(
        StringFilter.Factory stringFilterFactory,
        YesNoChoiceFilter.Factory yesNoChoiceFactory,
        UintChoiceFilter.Factory uintChoiceFactory,
        FloatRangeFilter.Factory floatRangeFactory,
        UintRangeFilter.Factory uintRangeFactory,
        ItemInfoRenderService itemInfoRenderService,
        IDataManager dataManager,
        FilterState filterState,
        ItemSheet itemSheet,
        ItemInfoCache itemInfoCache)
    {
        this.cancellationTokenSource = new CancellationTokenSource();
        this.defaultItemList = itemSheet.Where(c => c.NameString != string.Empty).OrderBy(c => c.NameString).ToList();
        this.stringFilterFactory = stringFilterFactory;
        this.yesNoChoiceFactory = yesNoChoiceFactory;
        this.uintChoiceFactory = uintChoiceFactory;
        this.floatRangeFactory = floatRangeFactory;
        this.uintRangeFactory = uintRangeFactory;
        this.itemInfoRenderService = itemInfoRenderService;
        this.dataManager = dataManager;
        this.filterState = filterState;
        this.itemInfoCache = itemInfoCache;
        this.AddFilters();
    }

    public List<IItemFilter> Filters
    {
        get => this.filters;
    }

    private void AddFilters()
    {
        this.filters.Add(this.stringFilterFactory.Invoke("name", "Name", "The name of the item", row => row.NameString));
        this.filters.Add(this.uintChoiceFactory.Invoke("category", "Category", "The category of the item", row => row.Base.ItemUICategory.RowId, this.dataManager.GetExcelSheet<ItemUICategory>().Where(c => c.RowId != 0 && c.Name.ExtractText() != string.Empty).ToDictionary(c => c.RowId, c => c.Name.ExtractText())));
        this.filters.Add(this.uintRangeFactory.Invoke("equipLevel", "Equip Level", "The equip level of the item", row => row.Base.LevelEquip, 1, 100));
        this.filters.Add(this.uintRangeFactory.Invoke("itemLevel", "Item Level", "The item's iLvl", row => row.Base.LevelItem.RowId, 1, this.dataManager.GetExcelSheet<Item>().MaxBy(c => c.LevelItem.RowId).LevelItem.RowId));
        this.filters.Add(
            this.uintChoiceFactory.Invoke(
                "rarity",
                "Rarity",
                "The rarity of the item",
                row => row.Base.Rarity,
                new Dictionary<uint, string>()
        {
            { 1, "Normal" },
            { 2, "Scarce" },
            { 3, "Artifact" },
            { 4, "Relic" },
            { 7, "Aetherial" },
        }));

        this.filters.Add(
            this.uintChoiceFactory.Invoke(
                "classRole",
                "Class Role",
                "The class/job role",
                row => row.ClassJobCategory?.ClassJobs.FirstOrDefault()
                          ?.Base.RowId ??
                       0,
                new Dictionary<uint, string>()
        {
            { 0, "Non-combatant" },
            { 1, "Tank" },
            { 2, "Melee DPS" },
            { 3, "Ranged DPS" },
            { 4, "Healer" },
        }));

        this.filters.Add(
            this.uintChoiceFactory.Invoke(
                "race",
                "Race",
                "The race required to equip the item",
                row => (uint)row.EquipRace,
                new Dictionary<uint, string>()
        {
            { (uint)CharacterRace.Hyur, "Hyur" },
            { (uint)CharacterRace.Elezen, "Elezen" },
            { (uint)CharacterRace.Hrothgar, "Hrothgar" },
            { (uint)CharacterRace.Lalafell, "Lalafell" },
            { (uint)CharacterRace.Miqote, "Miqote" },
            { (uint)CharacterRace.Roegadyn, "Roegadyn" },
            { (uint)CharacterRace.Viera, "Viera" },
            { (uint)CharacterRace.AuRa, "AuRa" },
        }));

        this.filters.Add(
            this.uintChoiceFactory.Invoke(
                "sex",
                "Sex",
                "The sex required to equip the item",
                row => (uint)row.EquippableByGender,
                new Dictionary<uint, string>()
        {
            { (uint)CharacterSex.Male, "Male" },
            { (uint)CharacterSex.Female, "Female" },
        }));

        this.filters.Add(
            this.yesNoChoiceFactory.Invoke(
                "canBeHq",
                "Can be HQ?",
                "Can this item be high quality?",
                row => row.Base.CanBeHq));

        this.filters.Add(
            this.yesNoChoiceFactory.Invoke(
                "isUnique",
                "Is Unique?",
                "Is this item unique?",
                row => row.Base.IsUnique));

        this.filters.Add(
            this.yesNoChoiceFactory.Invoke(
                "tradable",
                "Is Tradable?",
                "Is this item tradable?",
                row => !row.Base.IsUntradable));

        this.filters.Add(
            this.yesNoChoiceFactory.Invoke(
                "equippable",
                "Is Equippable?",
                "Can the item be equipped?",
                row => row.CanTryOn));

        var sourceUseFilters = new List<IItemFilter>();

        foreach (var source in Enum.GetValues<ItemInfoType>())
        {
            if (this.itemInfoRenderService.SourceRenderersByItemInfoType.ContainsKey(source))
            {
                var renderer = this.itemInfoRenderService.SourceRenderersByItemInfoType[source];
                var yesNoChoiceFilter = this.yesNoChoiceFactory.Invoke(
                    "s_" + source,
                    "Source - " + renderer.SingularName,
                    renderer.HelpText,
                    row => row.HasSourcesByType(source));
                yesNoChoiceFilter.RendererType = renderer.RendererType;
                yesNoChoiceFilter.Categories = renderer.Categories;
                sourceUseFilters.Add(
                    yesNoChoiceFilter);
            }
        }

        foreach (var source in Enum.GetValues<ItemInfoType>())
        {
            if (this.itemInfoRenderService.UseRenderersByItemInfoType.ContainsKey(source))
            {
                var renderer = this.itemInfoRenderService.UseRenderersByItemInfoType[source];
                var yesNoChoiceFilter = this.yesNoChoiceFactory.Invoke(
                    "u_" + source,
                    "Use - " + renderer.SingularName,
                    renderer.HelpText,
                    row => row.HasUsesByType(source));
                yesNoChoiceFilter.RendererType = renderer.RendererType;
                yesNoChoiceFilter.Categories = renderer.Categories;
                sourceUseFilters.Add(
                    yesNoChoiceFilter);
            }
        }

        this.filters.AddRange(sourceUseFilters.OrderBy(c => c.Name));
    }

    public List<ItemRow> GetFilteredItems()
    {
        if (this.filterState.IsDirty)
        {
            this.filterState.IsDirty = false;
            this.filterTask = (Task<List<ItemRow>>?)Task.Run(() => this.GenerateFilteredItemsList(this.cancellationTokenSource.Token), this.cancellationTokenSource.Token);
        }

        if (this.filterTask?.IsCompleted == true)
        {
            this.itemRows = this.filterTask.Result;
            this.filterTask = null;
        }

        return this.itemRows;
    }

    private List<ItemRow> GenerateFilteredItemsList(CancellationToken token)
    {
        IEnumerable<ItemRow> allItems = this.defaultItemList.OrderBy(c => c.NameString).ToList();
        foreach (var filter in this.filters)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }
            if (filter.HasValueSet(this.filterState))
            {
                allItems = filter.Match(this.filterState, allItems);
            }
        }

        return allItems.ToList();
    }

    public void Dispose()
    {
        this.cancellationTokenSource.Dispose();
    }
}
