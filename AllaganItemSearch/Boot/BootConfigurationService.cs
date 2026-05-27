using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Dalamud.Plugin;
using Dalamud.Plugin.Services;

using Microsoft.Extensions.Hosting;

using Newtonsoft.Json;

namespace AllaganItemSearch.Boot;

public sealed class BootConfigurationService : IHostedService, IAsyncDisposable
{
    private readonly string configPath;
    private readonly IFramework framework;
    private readonly IPluginLog pluginLog;
    private Task saveTask = Task.CompletedTask;
    private volatile bool saveQueued;

    public BootConfigurationService(
        IDalamudPluginInterface pluginInterface,
        IFramework framework,
        IPluginLog pluginLog)
    {
        this.framework = framework;
        this.pluginLog = pluginLog;
        var pluginDir = pluginInterface.ConfigDirectory;
        this.configPath = Path.Combine(pluginDir.FullName, "boot.json");
    }

    public BootConfiguration Configuration { get; private set; }

    public async ValueTask DisposeAsync()
    {
        this.framework.Update -= this.OnFrameworkUpdate;
        await this.SaveInternal();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        this.Configuration = await this.Load();
        this.framework.Update += this.OnFrameworkUpdate;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        this.framework.Update -= this.OnFrameworkUpdate;

        if (this.Configuration.IsDirty)
        {
            await this.SaveInternal();
        }
    }

    private async Task<BootConfiguration> Load()
    {
        if (!File.Exists(this.configPath))
        {
            return new BootConfiguration();
        }

        try
        {
            var json = await File.ReadAllTextAsync(this.configPath);
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

        if (!this.saveTask.IsCompleted)
        {
            this.saveQueued = true;
            return;
        }

        this.saveTask = this.RunSave();
    }

    private async Task RunSave()
    {
        do
        {
            this.saveQueued = false;
            await this.SaveInternal();
        }
        while (this.saveQueued);
    }

    private async Task SaveInternal()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(this.configPath)!);

            var json = JsonConvert.SerializeObject(this.Configuration);
            await File.WriteAllTextAsync(this.configPath, json);
            this.pluginLog.Verbose("Saving allagan item search boot configuration.");

            this.Configuration.ClearDirty();
        }
        catch (Exception e)
        {
            this.pluginLog.Error("Failed to save allagan item search boot configuration.", e);
        }
    }
}
