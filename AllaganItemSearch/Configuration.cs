using System;
using System.Collections.Generic;
using System.ComponentModel;

using AllaganItemSearch.Settings;
using AllaganLib.Interface.Converters;
using AllaganLib.Interface.FormFields;
using AllaganLib.Interface.Wizard;
using Dalamud.Configuration;
using Newtonsoft.Json;

namespace AllaganItemSearch;

[Serializable]
public class Configuration : IPluginConfiguration, IConfigurable<int?>, IConfigurable<bool?>, IConfigurable<Enum?>,
                             IWizardConfiguration, IConfigurable<string?>, IConfigurable<FilterPanePosition>
{
    private HashSet<string>? wizardVersionsSeen1;
    private bool isConfigWindowMovable = true;
    private Dictionary<string, int> integerSettings = [];
    private Dictionary<string, bool> booleanSettings = [];
    private Dictionary<string, string> stringSettings = [];
    private Dictionary<string, Enum> enumSettings = [];
    private Dictionary<string, FilterPanePosition> filterPaneSettings = [];
    private HashSet<string> pinnedFields = [];
    private bool isDirty;
    private int version;
    private bool showWizardNewFeatures;

    public bool IsConfigWindowMovable
    {
        get => this.isConfigWindowMovable;
        set => this.isConfigWindowMovable = value;
    }

    public Dictionary<string, int> IntegerSettings
    {
        get => this.integerSettings;
        set => this.integerSettings = value;
    }

    public Dictionary<string, bool> BooleanSettings
    {
        get => this.booleanSettings;
        set => this.booleanSettings = value;
    }

    public Dictionary<string, string> StringSettings
    {
        get => this.stringSettings;
        set => this.stringSettings = value;
    }

    [JsonConverter(typeof(EnumDictionaryConverter))]
    public Dictionary<string, Enum> EnumSettings
    {
        get => this.enumSettings;
        set => this.enumSettings = value;
    }

    public bool IsDirty
    {
        get => this.isDirty;
        set => this.isDirty = value;
    }

    public int Version
    {
        get => this.version;
        set => this.version = value;
    }

    public HashSet<string> PinnedFields
    {
        get => this.pinnedFields;
        set => this.pinnedFields = value;
    }

    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate, NullValueHandling = NullValueHandling.Ignore)]
    [DefaultValue(true)]
    public bool ShowWizardNewFeatures
    {
        get => this.showWizardNewFeatures;
        set
        {
            this.showWizardNewFeatures = value;
            this.isDirty = true;
        }
    }

    public HashSet<string> WizardVersionsSeen
    {
        get => this.WizardVersionsSeen1 ??= [];
        set
        {
            this.WizardVersionsSeen1 = value;
            this.IsDirty = true;
        }
    }

    private HashSet<string>? WizardVersionsSeen1
    {
        get => this.wizardVersionsSeen1;
        set => this.wizardVersionsSeen1 = value;
    }

    public void MarkWizardVersionSeen(string versionNumber)
    {
        this.WizardVersionsSeen.Add(versionNumber);
        this.IsDirty = true;
    }

    public int? Get(string key)
    {
        return this.IntegerSettings.TryGetValue(key, out var value) ? value : null;
    }

    public void Set(string key, FilterPanePosition newValue)
    {
        this.filterPaneSettings[key] = newValue;

        this.IsDirty = true;
    }

    public void Set(string key, string? newValue)
    {
        if (newValue == null)
        {
            this.StringSettings.Remove(key);
        }
        else
        {
            this.StringSettings[key] = newValue;
        }

        this.IsDirty = true;
    }

    public void Set(string key, Enum? newValue)
    {
        if (newValue == null)
        {
            this.EnumSettings.Remove(key);
        }
        else
        {
            this.EnumSettings[key] = newValue;
        }

        this.IsDirty = true;
    }

    public void Set(string key, bool? newValue)
    {
        if (newValue == null)
        {
            this.BooleanSettings.Remove(key);
        }
        else
        {
            this.BooleanSettings[key] = newValue.Value;
        }

        this.IsDirty = true;
    }

    public void Set(string key, int? newValue)
    {
        if (newValue == null)
        {
            this.IntegerSettings.Remove(key);
        }
        else
        {
            this.IntegerSettings[key] = newValue.Value;
        }

        this.IsDirty = true;
    }

    bool? IConfigurable<bool?>.Get(string key)
    {
        return this.BooleanSettings.TryGetValue(key, out var value) ? value : null;
    }

    Enum? IConfigurable<Enum?>.Get(string key)
    {
        return this.EnumSettings.GetValueOrDefault(key);
    }

    string? IConfigurable<string?>.Get(string key)
    {
        return this.StringSettings.GetValueOrDefault(key);
    }

    public bool IsFieldPinned(string fieldKey)
    {
        return this.pinnedFields.Contains(fieldKey);
    }

    public void PinField(string fieldKey)
    {
        if (this.pinnedFields.Add(fieldKey))
        {
            this.IsDirty = true;
        }
    }

    public void UnpinField(string fieldKey)
    {
        if (this.pinnedFields.Remove(fieldKey))
        {
            this.IsDirty = true;
        }
    }

    public void ToggleFieldPinning(string fieldKey)
    {
        if (this.IsFieldPinned(fieldKey))
        {
            this.UnpinField(fieldKey);
        }
        else
        {
            this.PinField(fieldKey);
        }
    }

    FilterPanePosition IConfigurable<FilterPanePosition>.Get(string key)
    {
        return this.filterPaneSettings.GetValueOrDefault(key);
    }
}
