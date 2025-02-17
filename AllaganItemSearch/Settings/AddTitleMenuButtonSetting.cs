using AllaganLib.Interface.FormFields;
using AllaganLib.Interface.Services;

namespace AllaganItemSearch.Settings;

public class AddTitleMenuButtonSetting(ImGuiService imGuiService)
    : BooleanFormField<Configuration>(imGuiService), ISetting
{
    public override bool DefaultValue { get; set; } = false;

    public override string Key { get; set; } = "AddTitleMenuButton";

    public override string Name { get; set; } = "Add title menu button?";

    public override string HelpText { get; set; } =
        "Should a button to open Allagan Item Search be added to Dalamud's title menu?";

    public override string Version { get; } = "1.0.0";

    public SettingType Type { get; set; } = SettingType.Features;
}
