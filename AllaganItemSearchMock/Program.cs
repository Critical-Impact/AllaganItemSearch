using DalaMock.Core.Plugin;

namespace AllaganItemSearchMock;

internal class Program
{
    private static void Main(string[] args)
    {
        var mockContainer = new MockContainer();
        var mockDalamudUi = mockContainer.GetMockUi();
        var pluginLoader = mockContainer.GetPluginLoader();
        var mockPlugin = pluginLoader.AddPlugin(typeof(AllaganItemSearchPluginMock));
        mockDalamudUi.Run();
    }
}
