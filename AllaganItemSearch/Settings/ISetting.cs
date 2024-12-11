using AllaganLib.Interface.FormFields;

namespace AllaganItemSearch.Settings;

public interface ISetting : IFormField<Configuration>
{
    public SettingType Type { get; set; }
}
