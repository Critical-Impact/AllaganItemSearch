using System;

using AllaganItemSearch.Services;

using DalaMock.Host.Mediator;

using Dalamud.Interface.Windowing;

using Dalamud.Bindings.ImGui;

namespace AllaganItemSearch.Windows;

public abstract class ExtendedWindow(
    MediatorService mediatorService,
    ImGuiService imGuiService,
    string name,
    ImGuiWindowFlags flags = ImGuiWindowFlags.None,
    bool forceMainWindow = false) : Window(name, flags, forceMainWindow), IMediatorSubscriber, IDisposable
{
    public ImGuiService ImGuiService { get; } = imGuiService;

    public MediatorService MediatorService { get; } = mediatorService;

    public virtual void Dispose()
    {
        this.MediatorService.UnsubscribeAll(this);
    }
}
