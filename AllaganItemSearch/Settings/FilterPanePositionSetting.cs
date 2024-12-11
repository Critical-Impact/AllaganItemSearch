using System.Collections.Generic;

using AllaganLib.Interface.FormFields;
using AllaganLib.Interface.Services;

namespace AllaganItemSearch.Settings;

public enum FilterPanePosition
{
    Right,
    Left,
    Bottom,
    Top
}

public class FilterPanePositionSetting : ChoiceFormField<FilterPanePosition, Configuration>, ISetting
{
    public FilterPanePositionSetting(ImGuiService imGuiService) : base(imGuiService)
    {
    }

    public override FilterPanePosition DefaultValue { get; set; } = FilterPanePosition.Right;

    public override string Key { get; set; } = "FilterPanePosition";

    public override string Name { get; set; } = "Filter Pane Position";

    public override string HelpText { get; set; } = "What side should the filter pane be positioned?";

    public override string Version { get; } = "1.0.0.0";

    public override Dictionary<FilterPanePosition, string> Choices => new Dictionary<FilterPanePosition, string>
    {
        { FilterPanePosition.Left, "Left" },
        { FilterPanePosition.Right, "Right" },
        { FilterPanePosition.Bottom, "Bottom" },
        { FilterPanePosition.Top, "Top" },
    };

    public SettingType Type { get; set; } = SettingType.Layout;
}
