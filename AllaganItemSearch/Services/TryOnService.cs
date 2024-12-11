using System;
using System.Collections.Generic;

using AllaganLib.GameSheets.Sheets.Rows;

using Dalamud.Plugin.Services;

using FFXIVClientStructs.FFXIV.Client.UI.Agent;

namespace AllaganItemSearch.Services
{
    public class TryOnService : IDisposable, ITryOnService
    {
        private readonly IFramework framework;
        private readonly IPluginLog pluginLog;
        private readonly Queue<(uint Itemid, byte StainId)> tryOnQueue = new();
        private int tryOnDelay = 10;
        private bool disposed;

        public TryOnService(IFramework framework, IPluginLog pluginLog)
        {
            this.framework = framework;
            this.pluginLog = pluginLog;
            this.CanUseTryOn = true;
            framework.Update += this.FrameworkUpdate;
        }

        public bool CanUseTryOn { get; }

        public void TryOnItem(ItemRow item, byte stainId = 0, bool hq = false)
        {
            if (item.EquipSlotCategory == null)
            {
                return;
            }

            if (item.EquipSlotCategory.RowId > 0 && item.EquipSlotCategory.RowId != 6 &&
                item.EquipSlotCategory.RowId != 17 &&
                (item.EquipSlotCategory?.Base.OffHand <= 0 || item.Base.ItemUICategory.RowId == 11))
            {
                this.tryOnQueue.Enqueue((item.RowId + (uint)(hq ? 1000000 : 0), stainId));
            }
            else
            {
                this.pluginLog.Error(
                    $"Cancelled Try On: Invalid Item. ({item.EquipSlotCategory?.RowId}, {item.EquipSlotCategory?.Base.OffHand}, {item.EquipSlotCategory?.Base.Waist}, {item.EquipSlotCategory?.Base.SoulCrystal})");
            }
        }

        public void OpenFittingRoom()
        {
            this.tryOnQueue.Enqueue((0, 0));
        }

        public void FrameworkUpdate(IFramework framework)
        {
            while (this.CanUseTryOn && this.tryOnQueue.Count > 0 && (this.tryOnDelay <= 0 || this.tryOnDelay-- <= 0))
            {
                try
                {
                    var (itemId, stainId) = this.tryOnQueue.Dequeue();
                    this.tryOnDelay = 1;
                    AgentTryon.TryOn(0, itemId, stainId, 0, 0);
                }
                catch
                {
                    this.tryOnDelay = 5;
                    break;
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                if (this.CanUseTryOn)
                {
                    this.framework.Update -= this.FrameworkUpdate;
                }
            }

            this.disposed = true;
        }
    }
}
