using System.Threading;
using System.Threading.Tasks;

using AllaganLib.Interface.Wizard;

using AllaganItemSearch.Mediator;
using AllaganItemSearch.Windows;

using DalaMock.Host.Mediator;

using Dalamud.Plugin.Services;

using Microsoft.Extensions.Hosting;

namespace AllaganItemSearch.Services;

/// <summary>
/// Handles plugin bootup and teardown.
/// </summary>
public class PluginBootService(
    Configuration configuration,
    MediatorService mediatorService,
    WizardWindow wizardWindow,
    IConfigurationWizardService<Configuration> configurationWizardService,
    IClientState clientState) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        clientState.Login += this.ClientLoggedIn;
        if (clientState.IsLoggedIn)
        {
            this.ClientLoggedIn();
        }

        mediatorService.Publish(new PluginLoadedMessage());
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        clientState.Login -= this.ClientLoggedIn;
        return Task.CompletedTask;
    }

    private void ClientLoggedIn()
    {
        if (configurationWizardService.ShouldShowWizard || !configurationWizardService.ConfiguredOnce)
        {
            wizardWindow.IsOpen = true;
        }
    }
}
