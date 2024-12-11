using System.Collections.Generic;

using AllaganLib.Interface.FormFields;

namespace AllaganItemSearch.Filters;

public class FilterState : IConfigurable<int?>, IConfigurable<bool?>, IConfigurable<string?>, IConfigurable<YesNoChoice>, IConfigurable<List<uint>?>, IConfigurable<(float Min, float Max)?>, IConfigurable<(uint Min, uint Max)?>
{
    private Dictionary<string, int> integerSettings = [];
    private Dictionary<string, bool> booleanSettings = [];
    private Dictionary<string, string> stringSettings = [];
    private Dictionary<string, YesNoChoice> yesNoChoiceEntries = [];
    private Dictionary<string, List<uint>> uintListSettings = [];
    private Dictionary<string, (float, float)> floatRangeSettings = [];
    private Dictionary<string, (uint, uint)> uintRangeSettings = [];
    private bool isDirty = true;

    public Dictionary<string, int> IntegerSettings
    {
        get => this.integerSettings;
    }

    public Dictionary<string, bool> BooleanSettings
    {
        get => this.booleanSettings;
    }

    public Dictionary<string, string> StringSettings
    {
        get => this.stringSettings;
    }

    public Dictionary<string, YesNoChoice> YesNoChoiceEntries
    {
        get => this.yesNoChoiceEntries;
    }

    public bool IsDirty
    {
        get => this.isDirty;
        set => this.isDirty = value;
    }

    public int? Get(string key)
    {
        return this.IntegerSettings.TryGetValue(key, out var value) ? value : null;
    }

    public void Set(string key, (float Min, float Max)? newValue)
    {
        if (newValue == null)
        {
            this.floatRangeSettings.Remove(key);
        }
        else
        {
            this.floatRangeSettings[key] = newValue.Value;
        }

        this.IsDirty = true;
    }

    public void Set(string key, (uint Min, uint Max)? newValue)
    {
        if (newValue == null)
        {
            this.uintRangeSettings.Remove(key);
        }
        else
        {
            this.uintRangeSettings[key] = newValue.Value;
        }

        this.IsDirty = true;
    }

    public void Set(string key, List<uint>? newValue)
    {
        if (newValue == null)
        {
            this.uintListSettings.Remove(key);
        }
        else
        {
            this.uintListSettings[key] = newValue;
        }
        this.IsDirty = true;
    }

    public void Set(string key, YesNoChoice newValue)
    {
        this.YesNoChoiceEntries[key] = newValue;
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

    string? IConfigurable<string?>.Get(string key)
    {
        return this.StringSettings.TryGetValue(key, out var value) ? value : null;
    }

    YesNoChoice IConfigurable<YesNoChoice>.Get(string key)
    {
        return this.YesNoChoiceEntries.TryGetValue(key, out var value) ? value : YesNoChoice.NA;
    }

    List<uint>? IConfigurable<List<uint>?>.Get(string key)
    {
        return this.uintListSettings.TryGetValue(key, out var value) ? value : null;
    }

    (float Min, float Max)? IConfigurable<(float Min, float Max)?>.Get(string key)
    {
        return this.floatRangeSettings.TryGetValue(key, out var value) ? value : null;
    }

    (uint Min, uint Max)? IConfigurable<(uint Min, uint Max)?>.Get(string key)
    {
        return this.uintRangeSettings.TryGetValue(key, out var value) ? value : null;
    }
}
