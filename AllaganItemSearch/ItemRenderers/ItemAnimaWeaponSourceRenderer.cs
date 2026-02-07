using System;
using System.Linq;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;

namespace AllaganItemSearch.ItemRenderers;

public class ItemAnimaWeaponSourceRenderer : ItemInfoRenderer<ItemAnimaWeaponSource>
{
    public override RendererType RendererType => RendererType.Use;

    public override ItemInfoType Type => ItemInfoType.AnimaWeapon;

    public override string SingularName => "Anima Weapon";

    public override string HelpText => "Is this a Anima Weapon?";

    public override bool ShouldGroup => false;

    public override Action<ItemSource> DrawTooltip => source => { };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = this.AsSource(source);
        return string.Join(", ", asSource.Items.Select(c => c.NameString));
    };

    public override Func<ItemSource, int> GetIcon => _ => AllaganLib.Shared.Misc.Icons.SwordIcon;
}
