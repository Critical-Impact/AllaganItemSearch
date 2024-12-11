using AllaganLib.GameSheets.Sheets.Rows;

namespace AllaganItemSearch.Services;

public interface ITryOnService
{
    bool CanUseTryOn { get; }

    void TryOnItem(ItemRow item, byte stainId = 0, bool hq = false);

    void OpenFittingRoom();
}
