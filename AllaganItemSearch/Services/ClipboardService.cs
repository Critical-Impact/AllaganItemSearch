using ImGuiNET;

namespace AllaganItemSearch.Services;

public class ClipboardService : IClipboardService
{
    public void CopyToClipboard(string text)
    {
        ImGui.SetClipboardText(text);
    }

    public string PasteFromClipboard()
    {
        return ImGui.GetClipboardText();
    }
}
