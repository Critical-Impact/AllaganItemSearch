using AllaganItemSearch.Services;

using AllaganLib.GameSheets.Sheets.Rows;

using Dalamud.Plugin.Services;

namespace AllaganItemSearchMock;

public class MockTryOnService : ITryOnService
{
    private readonly IPluginLog pluginLog;

    public bool CanUseTryOn { get; set; } = true;

    public MockTryOnService(IPluginLog pluginLog)
    {
        this.pluginLog = pluginLog;
    }

    public void TryOnItem(ItemRow item, byte stainId = 0, bool hq = false)
    {
        this.pluginLog.Verbose("Tried to try on " + item.RowId);
    }

    public void OpenFittingRoom()
    {
        this.pluginLog.Verbose("Opened fitting room");
    }
}
