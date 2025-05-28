using System;
using System.Collections.Generic;
using System.Linq;

using AllaganItemSearch.ItemRenderers;

using AllaganLib.GameSheets.Caches;

using Dalamud.Plugin.Services;

namespace AllaganItemSearch.Services;

public class ItemInfoRenderService
{
    private readonly ImGuiService imGuiService;
    private readonly IPluginLog pluginLog;
    private readonly Dictionary<ItemInfoType, IItemInfoRenderer> sourceRenderersByItemInfoType;
    private readonly Dictionary<ItemInfoType, IItemInfoRenderer> useRenderersByItemInfoType;

    public ItemInfoRenderService(
        IEnumerable<IItemInfoRenderer> itemRenderers,
        ImGuiService imGuiService,
        IPluginLog pluginLog)
    {
        this.imGuiService = imGuiService;
        this.pluginLog = pluginLog;
        var itemInfoRenderers = itemRenderers.ToList();
        this.sourceRenderersByItemInfoType = itemInfoRenderers.Where(c => c.RendererType == RendererType.Source)
                                                              .ToDictionary(c => c.Type, c => c);
        this.useRenderersByItemInfoType = itemInfoRenderers.Where(c => c.RendererType == RendererType.Use)
                                                           .ToDictionary(c => c.Type, c => c);

#if DEBUG
        foreach (var itemType in Enum.GetValues<ItemInfoType>())
        {
            if (!this.sourceRenderersByItemInfoType.ContainsKey(itemType) && !this.useRenderersByItemInfoType.ContainsKey(itemType))
            {
                this.pluginLog.Verbose($"Missing type {itemType}");
            }
        }
#endif
    }

    public Dictionary<ItemInfoType, IItemInfoRenderer> UseRenderersByItemInfoType => this.useRenderersByItemInfoType;

    public Dictionary<ItemInfoType, IItemInfoRenderer> SourceRenderersByItemInfoType =>
        this.sourceRenderersByItemInfoType;

    public IItemInfoRenderer? GetSourceRendererByItemInfoType(ItemInfoType itemInfoType)
    {
        return this.sourceRenderersByItemInfoType.GetValueOrDefault(itemInfoType);
    }

    public IItemInfoRenderer? GetUseRendererByItemInfoType(ItemInfoType itemInfoType)
    {
        return this.useRenderersByItemInfoType.GetValueOrDefault(itemInfoType);
    }
}
