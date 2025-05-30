
using AllaganItemSearch.Services;

namespace AllaganItemSearchMock;

public class MockClipboardService : IClipboardService
{
    public void CopyToClipboard(string text)
    {
        TextCopy.ClipboardService.SetText(text);
    }

    public string PasteFromClipboard()
    {
        return TextCopy.ClipboardService.GetText() ?? string.Empty;
    }
}
