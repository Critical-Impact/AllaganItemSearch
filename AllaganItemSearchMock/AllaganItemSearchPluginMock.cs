using AllaganItemSearch;
using AllaganItemSearch.Services;

using AllaganLib.Shared.Time;

using Autofac;

using DalaMock.Core.Mocks;
using DalaMock.Core.Windows;
using DalaMock.Shared.Interfaces;

using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

using Microsoft.Extensions.DependencyInjection;

namespace AllaganItemSearchMock;

public class AllaganItemSearchPluginMock(
    IDalamudPluginInterface pluginInterface,
    IPluginLog pluginLog,
    ICommandManager commandManager,
    ITextureProvider textureProvider,
    IGameInteropProvider gameInteropProvider,
    IAddonLifecycle addonLifecycle,
    IClientState clientState,
    IGameInventory gameInventory,
    IFramework framework,
    IDataManager dataManager,
    IChatGui chatGui,
    IMarketBoard marketBoard,
    ITitleScreenMenu titleScreenMenu,
    IDtrBar dtrBar,
    IGameGui gameGui,
    IKeyState keyState) : AllaganItemSearchPlugin(
    pluginInterface,
    pluginLog,
    commandManager,
    textureProvider,
    gameInteropProvider,
    addonLifecycle,
    clientState,
    gameInventory,
    framework,
    dataManager,
    chatGui,
    marketBoard,
    titleScreenMenu,
    dtrBar,
    gameGui,
    keyState)
{
    public override void ConfigureContainer(ContainerBuilder containerBuilder)
    {
        base.ConfigureContainer(containerBuilder);
        containerBuilder.RegisterType<MockWindowSystem>().AsSelf().As<IWindowSystem>().SingleInstance();
        containerBuilder.RegisterType<MockFileDialogManager>().AsSelf().As<IFileDialogManager>().SingleInstance();
        containerBuilder.RegisterType<MockFont>().AsSelf().As<IFont>().SingleInstance();
        containerBuilder.RegisterType<MockWindow>().AsSelf().As<Window>().SingleInstance();
        containerBuilder.RegisterType<MockBootService>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<MockSeTime>().As<ISeTime>().SingleInstance();
        containerBuilder.RegisterType<MockTryOnService>().As<ITryOnService>().SingleInstance();
        containerBuilder.RegisterType<MockClipboardService>().As<IClipboardService>().SingleInstance();
    }

    public override void ConfigureServices(IServiceCollection serviceCollection)
    {
        base.ConfigureServices(serviceCollection);
        serviceCollection.AddHostedService<MockBootService>(c => c.GetRequiredService<MockBootService>());
    }
}