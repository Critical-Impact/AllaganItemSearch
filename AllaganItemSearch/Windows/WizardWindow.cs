using System;
using System.Numerics;

using AllaganLib.Interface.Widgets;

using AllaganItemSearch.Services;

using DalaMock.Host.Mediator;

using ImGuiNET;

namespace AllaganItemSearch.Windows;

public class WizardWindow : ExtendedWindow, IDisposable
{
    private readonly WizardWidget<Configuration> wizardWidget;

    public WizardWindow(
        WizardWidget<Configuration> wizardWidget,
        MediatorService mediatorService,
        ImGuiService imGuiService)
        : base(mediatorService, imGuiService, "Allagan Item Search - Wizard")
    {
        this.wizardWidget = wizardWidget;
        this.wizardWidget.OnClosed += this.WizardWidgetOnOnClosed;
        this.Size = new Vector2(800, 500);
        this.SizeCondition = ImGuiCond.FirstUseEver;
        this.SizeConstraints = new WindowSizeConstraints()
        {
            MinimumSize = new Vector2(800, 350),
            MaximumSize = new Vector2(1000, 1000),
        };
    }

    public override void OnOpen()
    {
        this.wizardWidget.Initialize();
        base.OnOpen();
    }

    public override void Draw()
    {
        this.wizardWidget.Draw();
    }

    public new void Dispose()
    {
        this.Dispose(true);
        base.Dispose();
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.wizardWidget.OnClosed -= this.WizardWidgetOnOnClosed;
        }
    }

    private void WizardWidgetOnOnClosed()
    {
        this.IsOpen = false;
    }
}
