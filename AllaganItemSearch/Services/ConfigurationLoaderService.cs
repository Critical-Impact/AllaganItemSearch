using System;
using System.Threading;
using System.Threading.Tasks;

using AllaganLib.Data.Service;

using Dalamud.Plugin;
using Dalamud.Plugin.Services;

using Microsoft.Extensions.Hosting;

namespace AllaganItemSearch.Services;

public class ConfigurationLoaderService(
    CsvLoaderService csvLoaderService,
    IDalamudPluginInterface pluginInterface,
    IPluginLog pluginLog) : IHostedService
{
    private Configuration? configuration;

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Configuration GetConfiguration()
    {
        if (this.configuration == null)
        {
            try
            {
                this.configuration = pluginInterface.GetPluginConfig() as Configuration ??
                                     new Configuration();
            }
            catch (Exception e)
            {
                pluginLog.Error(e, "Failed to load configuration");
                this.configuration = new Configuration();
            }
        }

        return this.configuration;
    }

    public void Save()
    {
        this.GetConfiguration().IsDirty = false;
        pluginInterface.SavePluginConfig(this.GetConfiguration());
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        this.Save();
        pluginLog.Verbose("Stopping configuration loader, saving.");
        return Task.CompletedTask;
    }
}
