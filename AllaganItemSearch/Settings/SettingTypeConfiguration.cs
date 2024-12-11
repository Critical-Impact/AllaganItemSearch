using System.Collections.Generic;

using static AllaganItemSearch.Settings.SettingType;

namespace AllaganItemSearch.Settings;

public class SettingTypeConfiguration
{
    private List<SettingType>? settingTypes;

    public static string GetFormattedName(SettingType settingType)
    {
        return settingType switch
        {
            General => "General",
            SettingType.Features => "Features",
            Mqtt => "MQTT",
            _ => settingType.ToString(),
        };
    }

    public List<SettingType> GetCategoryOrder()
    {
        return this.settingTypes ??= [General, SettingType.Features, Mqtt];
    }
}
