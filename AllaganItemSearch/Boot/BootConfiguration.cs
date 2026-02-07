using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AllaganItemSearch.Boot;

public sealed class BootConfiguration
{
    [DefaultValue(true)]
    private bool persistLuminaCache = true;

    [JsonIgnore]
    public bool IsDirty { get; private set; }

    public bool PersistLuminaCache
    {
        get => this.persistLuminaCache;
        set
        {
            if (this.persistLuminaCache == value)
            {
                return;
            }

            this.persistLuminaCache = value;
            this.IsDirty = true;
        }
    }

    public void ClearDirty()
    {
        this.IsDirty = false;
    }
}
