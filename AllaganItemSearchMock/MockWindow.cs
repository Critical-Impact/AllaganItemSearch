using AllaganItemSearch.Mediator;
using AllaganItemSearch.Services;
using AllaganItemSearch.Windows;

using DalaMock.Core.Mocks;
using DalaMock.Host.Mediator;

using ImGuiNET;

namespace AllaganItemSearchMock;

public class MockWindow : ExtendedWindow
{
    private readonly MockClientState mockClientState;

    public MockWindow(MediatorService mediatorService, ImGuiService imGuiService, MockClientState mockClientState)
        : base(mediatorService, imGuiService, "Mock Window")
    {
        this.mockClientState = mockClientState;
        this.IsOpen = true;
    }

    public override void Draw()
    {

    }
}
