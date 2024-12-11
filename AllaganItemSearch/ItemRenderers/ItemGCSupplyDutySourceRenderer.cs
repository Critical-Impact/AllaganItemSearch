using System;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;

using ImGuiNET;

namespace AllaganItemSearch.ItemRenderers;

public class ItemGcSupplyDutySourceRenderer : ItemInfoRenderer<ItemGCSupplyDutySource>
{
    public override RendererType RendererType => RendererType.Use;
    public override ItemInfoType Type => ItemInfoType.GCDailySupply;
    public override string SingularName => "Grand Company Supply & Provisioning";
    public override string HelpText => "Can the item be handed in for 'Supply & Provisioning' at your grand company?";
    public override bool ShouldGroup => true;

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = this.AsSource(source);
        var rewardRow = asSource.DailySupplyRewardRow;
        if (rewardRow != null)
        {
            var baseReward = rewardRow.Base.ExperienceSupply;
            var sealsSupply = rewardRow.Base.SealsSupply;
            ImGui.Text("Level: " + asSource.GCSupplyDutyRow.RowId);
            ImGui.Text("Exp: " + baseReward);
            ImGui.Text("Seals: " + sealsSupply);
        }
        else
        {
            ImGui.Text("Unknown rewards");
        }
    };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = this.AsSource(source);
        var rewardRow = asSource.DailySupplyRewardRow;
        return rewardRow != null ? asSource.GCSupplyDutyRow.RowId.ToString() : "";
    };
    public override Func<ItemSource, int> GetIcon => _ => Icons.FlameSealIcon;
}