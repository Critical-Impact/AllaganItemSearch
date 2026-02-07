using System;
using System.Linq;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;
using AllaganLib.GameSheets.Sheets;
using AllaganLib.Shared.Extensions;

using Dalamud.Bindings.ImGui;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace AllaganItemSearch.ItemRenderers;

public class ItemZodiacWeaponSourceRenderer : ItemInfoRenderer<ItemZodiacWeaponSource>
{
    public override RendererType RendererType => RendererType.Use;

    public override ItemInfoType Type => ItemInfoType.ZodiacWeapon;

    public override string SingularName => "Zodiac Weapon";

    public override string HelpText => "Is this a Zodiac Weapon?";

    public override bool ShouldGroup => false;

    public override Action<ItemSource> DrawTooltip => source =>
    {
    };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = this.AsSource(source);
        return string.Join(", ", asSource.Items.Select(c => c.NameString));
    };

    public override Func<ItemSource, int> GetIcon => _ => AllaganLib.Shared.Misc.Icons.SwordIcon;
}
