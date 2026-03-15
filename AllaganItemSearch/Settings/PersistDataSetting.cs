using AllaganItemSearch.Boot;
using AllaganItemSearch.Services;

using AllaganLib.Interface.FormFields;

namespace AllaganItemSearch.Settings;

public class PersistDataSetting(ImGuiService imGuiService, BootConfiguration bootConfiguration) : BooleanFormField<Configuration>(imGuiService), ISetting
{
    private readonly BootConfiguration bootConfiguration = bootConfiguration;

    public override bool DefaultValue { get; set; } = true;

    public override string Key { get; set; } = "PersistData";

    public override string Name { get; set; } = "Persist Cached Data";

    public override string HelpText { get; set; } =
        "Allagan Item Search has to calculate information when it first boots that can take upwards of 5-10 seconds depending on your computer. If this is on, that data is persisted between updates speeding up the boot time of the plugin.";

    public override string Version { get; set; } = "2.0.1";

    public SettingType Type { get; set; } = SettingType.Troubleshooting;

    public override bool CurrentValue(Configuration configurable)
    {
        return this.bootConfiguration.PersistLuminaCache;
    }

    public override void UpdateFilterConfiguration(Configuration configurable, bool newValue)
    {
        this.bootConfiguration.PersistLuminaCache = newValue;
    }
}
