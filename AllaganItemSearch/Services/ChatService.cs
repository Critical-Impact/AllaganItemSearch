using System.Collections.Generic;

using AllaganLib.GameSheets.Sheets.Helpers;
using AllaganLib.GameSheets.Sheets.Rows;

using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Plugin.Services;

namespace AllaganItemSearch.Services;

public class ChatService
{
    private readonly IKeyState keyState;
    private readonly IChatGui chatGui;

    public ChatService(IKeyState keyState, IChatGui chatGui)
    {
        this.keyState = keyState;
        this.chatGui = chatGui;
    }

    public void LinkItem(ItemRow item)
    {
        if (item.RowId == HardcodedItems.FreeCompanyCreditItemId)
        {
            return;
        }

        var payloadList = new List<Payload>
        {
            new UIForegroundPayload((ushort)(0x223 + (item.Base.Rarity * 2))),
            new UIGlowPayload((ushort)(0x224 + (item.Base.Rarity * 2))),
            new ItemPayload(item.RowId, item.Base.CanBeHq && this.keyState[0x11]),
            new UIForegroundPayload(500),
            new UIGlowPayload(501),
            new TextPayload($"{(char)SeIconChar.LinkMarker}"),
            new UIForegroundPayload(0),
            new UIGlowPayload(0),
            new TextPayload(
                item.Base.Name.ExtractText() +
                (item.Base.CanBeHq && this.keyState[0x11] ? $" {(char)SeIconChar.HighQuality}" : string.Empty)),
            new RawPayload([0x02, 0x27, 0x07, 0xCF, 0x01, 0x01, 0x01, 0xFF, 0x01, 0x03]),
            new RawPayload([0x02, 0x13, 0x02, 0xEC, 0x03]),
        };

        var payload = new SeString(payloadList);

        this.chatGui.Print(
            new XivChatEntry
            {
                Message = payload,
            });
    }
}
