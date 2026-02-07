using System;
using System.IO;

using Dalamud.Plugin;
using Dalamud.Plugin.Services;

using Newtonsoft.Json;

namespace AllaganItemSearch.Boot;

public sealed class BootConfigurationService : IDisposable
{
    private readonly string configPath;
    private readonly IFramework framework;
    private readonly IPluginLog pluginLog;

    public BootConfigurationService(
        IDalamudPluginInterface pluginInterface,
        IFramework framework,
        IPluginLog pluginLog)
    {
        this.framework = framework;
        this.pluginLog = pluginLog;
        var pluginDir = pluginInterface.ConfigDirectory;
        this.configPath = Path.Combine(pluginDir.FullName, "boot.json");
        this.Configuration = this.Load();
        framework.Update += this.OnFrameworkUpdate;
    }

    public BootConfiguration Configuration { get; }

    public void Dispose()
    {
        this.framework.Update -= this.OnFrameworkUpdate;

        if (this.Configuration.IsDirty)
        {
            this.SaveInternal();
        }
    }

    private BootConfiguration Load()
    {
        if (!File.Exists(this.configPath))
        {
            return new BootConfiguration();
        }

        try
        {
            var json = File.ReadAllText(this.configPath);
            return JsonConvert.DeserializeObject<BootConfiguration>(json)
                   ?? new BootConfiguration();
        }
        catch
        {
            return new BootConfiguration();
        }
    }

    private void OnFrameworkUpdate(IFramework _)
    {
        if (!this.Configuration.IsDirty)
        {
            return;
        }

        this.SaveInternal();
    }

    private void SaveInternal()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(this.configPath)!);

        var json = JsonConvert.SerializeObject(this.Configuration);
        File.WriteAllText(this.configPath, json);
        this.pluginLog.Verbose("Saving allagan tools boot configuration.");

        this.Configuration.ClearDirty();
    }

    public void Save()
    {
        this.SaveInternal();
    }
}
