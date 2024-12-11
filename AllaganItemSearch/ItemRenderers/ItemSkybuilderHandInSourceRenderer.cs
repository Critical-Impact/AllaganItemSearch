using System;

using AllaganLib.GameSheets.Caches;
using AllaganLib.GameSheets.ItemSources;
using AllaganLib.GameSheets.Sheets;

using Dalamud.Interface.Utility.Raii;

using ImGuiNET;

namespace AllaganItemSearch.ItemRenderers;

public class ItemSkybuilderHandInSourceRenderer : ItemInfoRenderer<ItemSkybuilderHandInSource>
{
    private readonly GatheringItemSheet gatheringItemSheet;

    public ItemSkybuilderHandInSourceRenderer(GatheringItemSheet gatheringItemSheet)
    {
        this.gatheringItemSheet = gatheringItemSheet;
    }
    public override RendererType RendererType => RendererType.Use;
    public override ItemInfoType Type => ItemInfoType.SkybuilderHandIn;
    public override string SingularName => "Sky Builder Hand In";
    public override string HelpText => "Can the item be handed in at the firmament for skybuilders' scrip?";
    public override bool ShouldGroup => false;

    public override Action<ItemSource> DrawTooltip => source =>
    {
        var asSource = this.AsSource(source);
        var baseReward = asSource.HWDCrafterSupplyParams.BaseCollectableReward.Value;
        var midReward = asSource.HWDCrafterSupplyParams.MidCollectableReward.Value;
        var highReward = asSource.HWDCrafterSupplyParams.HighCollectableReward.Value;
        ImGui.Text("Level: " + asSource.Level);
        ImGui.Text("Max Level: " + asSource.LevelMax);

        ImGui.Text("Rewards:");
        using (ImRaii.PushIndent())
        {
            ImGui.Text("Exp: " + baseReward.ExpReward + "/" + midReward.ExpReward + "/" + highReward.ExpReward);
            ImGui.Text("Script: " + baseReward.ScriptRewardAmount + "/" + midReward.ScriptRewardAmount + "/" +
                       highReward.ScriptRewardAmount);
            ImGui.Text("Points: " + baseReward.Points + "/" + midReward.Points + "/" + highReward.Points);
        }
    };

    public override Func<ItemSource, string> GetName => source =>
    {
        var asSource = this.AsSource(source);
        return asSource.Item.NameString;
    };
    public override Func<ItemSource, int> GetIcon => _ => Icons.SkybuildersScripIcon;
}