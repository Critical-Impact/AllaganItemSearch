using System;
using System.Globalization;
using System.Reflection;

using AllaganItemSearch.Boot;
using AllaganItemSearch.Filters;
using AllaganItemSearch.ItemRenderers;
using AllaganItemSearch.Services;
using AllaganItemSearch.Settings;
using AllaganItemSearch.Settings.Layout;
using AllaganItemSearch.Windows;

using AllaganLib.Data.Service;
using AllaganLib.GameSheets.Extensions;
using AllaganLib.GameSheets.Modules;
using AllaganLib.GameSheets.Service;
using AllaganLib.Interface.Services;
using AllaganLib.Interface.Widgets;
using AllaganLib.Interface.Wizard;
using AllaganLib.Shared.Time;

using Autofac;

using DalaMock.Host.Hosting;
using DalaMock.Shared.Classes;
using DalaMock.Shared.Interfaces;

using Dalamud.Game.Text;
using Dalamud.Interface.ImGuiFileDialog;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

using Lumina.Excel;
using Lumina.Excel.Sheets;

using Microsoft.Extensions.DependencyInjection;

using ImGuiService = AllaganItemSearch.Services.ImGuiService;

namespace AllaganItemSearch;

public class AllaganItemSearchPlugin : HostedPlugin
{
    private readonly BootConfigurationService bootService;

    public AllaganItemSearchPlugin(
        IDalamudPluginInterface pluginInterface,
        IPluginLog pluginLog,
        IFramework framework)
        : base(
            pluginInterface)
    {
        this.bootService = new BootConfigurationService(pluginInterface, framework, pluginLog);
        this.CreateHost();
        this.Start();
    }

    public override HostedPluginOptions ConfigureOptions()
    {
        return new HostedPluginOptions
        {
            UseMediatorService = true,
        };
    }

    public override void ConfigureContainer(ContainerBuilder containerBuilder)
    {
        var dataAccess = Assembly.GetExecutingAssembly();

        containerBuilder.Register(s =>
        {
            // Assume we only ever have one number format but we could just make this keyed later
            var gilNumberFormat =
                (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
            gilNumberFormat.CurrencySymbol = SeIconChar.Gil.ToIconString();
            gilNumberFormat.CurrencyDecimalDigits = 0;
            return gilNumberFormat;
        });

        containerBuilder.RegisterInstance(this.bootService).ExternallyOwned().AsSelf();
        containerBuilder.RegisterInstance(this.bootService.Configuration).ExternallyOwned().AsSelf();

        containerBuilder.RegisterAssemblyTypes(dataAccess)
                        .Where(t => t.Name.EndsWith("Setting"))
                        .AsSelf()
                        .AsImplementedInterfaces()
                        .SingleInstance();

        containerBuilder.RegisterAssemblyTypes(dataAccess)
                        .Where(t => t.Name.EndsWith("Feature"))
                        .AsSelf()
                        .AsImplementedInterfaces()
                        .SingleInstance();

        containerBuilder.RegisterAssemblyTypes(dataAccess)
                        .Where(t => t.Name.EndsWith("SettingLayout"))
                        .AsSelf()
                        .As<SettingPage>();

        // Services
        containerBuilder.RegisterType<WindowService>().SingleInstance();
        containerBuilder.RegisterType<InstallerWindowService>().SingleInstance();
        containerBuilder.RegisterType<ImGuiService>().AsSelf().As<AllaganLib.Interface.Services.ImGuiService>()
                        .SingleInstance();
        containerBuilder.RegisterType<CommandService>().SingleInstance();
        containerBuilder.RegisterType<CsvLoaderService>().SingleInstance();
        containerBuilder.RegisterType<AutoSaveService>().SingleInstance();
        containerBuilder.RegisterType<PluginBootService>().SingleInstance();
        containerBuilder.RegisterType<PluginStateService>().SingleInstance();
        containerBuilder.RegisterType<ConfigurationLoaderService>().SingleInstance();
        containerBuilder.RegisterType<SettingTypeConfiguration>().SingleInstance();
        containerBuilder.RegisterType<LaunchButtonService>().SingleInstance();
        containerBuilder.RegisterType<ChatService>().SingleInstance();
        containerBuilder.RegisterType<SeTime>().As<ISeTime>().SingleInstance();
        containerBuilder.RegisterType<ClipboardService>().As<IClipboardService>().SingleInstance();
        containerBuilder.RegisterType<TryOnService>().As<ITryOnService>().SingleInstance();
        containerBuilder.RegisterType<ATService>().SingleInstance();
        containerBuilder.RegisterModule(new GameSheetManagerModule()
        {
            StartupOptions = new SheetManagerStartupOptions()
            {
                PersistInDataShare = this.bootService.Configuration.PersistLuminaCache,
            },
        });

        containerBuilder.RegisterType<FileDialogManager>().SingleInstance();
        containerBuilder.RegisterType<DalamudFileDialogManager>().As<IFileDialogManager>().SingleInstance();

        // Windows
        containerBuilder.RegisterType<MainWindow>().As<Window>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<ConfigWindow>().As<Window>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<WizardWindow>().As<Window>().AsSelf().SingleInstance();

        // Hotkey System
        containerBuilder.RegisterType<HotkeyService<Configuration>>().AsSelf().AsImplementedInterfaces()
                        .SingleInstance();

        containerBuilder.Register(c => c.Resolve<IDataManager>().GameData).SingleInstance();

        // Custom Widgets
        containerBuilder.Register(c => new WizardWidgetSettings
        { PluginName = "Allagan Item Search", LogoPath = "logo_small" });
        containerBuilder.RegisterType<WizardWidget<Configuration>>().AsSelf().AsImplementedInterfaces()
                        .SingleInstance();
        containerBuilder.RegisterType<ConfigurationWizardService<Configuration>>().AsSelf().AsImplementedInterfaces()
                        .SingleInstance();
        containerBuilder.RegisterType<Font>().As<IFont>().SingleInstance();

        // Filters
        containerBuilder.RegisterType<FilterService>().SingleInstance();
        containerBuilder.RegisterType<StringFilter>();
        containerBuilder.RegisterType<YesNoChoiceFilter>();
        containerBuilder.RegisterType<UintChoiceFilter>();
        containerBuilder.RegisterType<FloatRangeFilter>();
        containerBuilder.RegisterType<UintRangeFilter>();
        containerBuilder.RegisterType<FilterState>().SingleInstance();
        containerBuilder.RegisterType<ItemInfoRenderService>().SingleInstance();

        containerBuilder.RegisterAssemblyTypes(dataAccess)
                        .Where(t => t.Name.EndsWith("Renderer"))
                        .AsSelf()
                        .As<IItemInfoRenderer>()
                        .SingleInstance();

        // Sheets
        containerBuilder.Register<ExcelSheet<Item>>(s =>
        {
            var dataManger = s.Resolve<IDataManager>();
            return dataManger.GetExcelSheet<Item>()!;
        }).SingleInstance();

        containerBuilder.Register<ExcelSheet<ClassJob>>(s =>
        {
            var dataManger = s.Resolve<IDataManager>();
            return dataManger.GetExcelSheet<ClassJob>()!;
        }).SingleInstance();

        containerBuilder.Register<ExcelSheet<World>>(s =>
        {
            var dataManger = s.Resolve<IDataManager>();
            return dataManger.GetExcelSheet<World>()!;
        }).SingleInstance();

        containerBuilder.Register(s =>
        {
            var configurationLoaderService = s.Resolve<ConfigurationLoaderService>();
            return configurationLoaderService.GetConfiguration();
        }).SingleInstance();
    }

    public override void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddHostedService(p => p.GetRequiredService<PluginBootService>());
        serviceCollection.AddHostedService(p => p.GetRequiredService<PluginStateService>());
        serviceCollection.AddHostedService(p => p.GetRequiredService<WindowService>());
        serviceCollection.AddHostedService(p => p.GetRequiredService<CommandService>());
        serviceCollection.AddHostedService(p => p.GetRequiredService<InstallerWindowService>());
        serviceCollection.AddHostedService(p => p.GetRequiredService<LaunchButtonService>());
        serviceCollection.AddHostedService(p => p.GetRequiredService<ConfigurationLoaderService>());
        serviceCollection.AddHostedService(p => p.GetRequiredService<AutoSaveService>());
        serviceCollection.AddHostedService(p => p.GetRequiredService<ATService>());
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.bootService.Dispose();
        }
    }

    public new void Dispose()
    {
        base.Dispose();
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
}
