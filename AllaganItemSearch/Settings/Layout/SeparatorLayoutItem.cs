using Autofac;

using ImGuiNET;

namespace AllaganItemSearch.Settings.Layout;

public class SeparatorLayoutItem : ISettingLayoutItem
{
    public void Draw(Configuration configuration, int? labelSize = null, int? inputSize = null)
    {
        ImGui.Separator();
    }

    public void Build(IComponentContext context)
    {
    }
}
