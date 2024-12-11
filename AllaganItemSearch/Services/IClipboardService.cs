namespace AllaganItemSearch.Services;

public interface IClipboardService
{
    public void CopyToClipboard(string text);
    public string PasteFromClipboard();
}
