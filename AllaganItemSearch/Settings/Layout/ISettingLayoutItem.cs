using Autofac;

namespace AllaganItemSearch.Settings.Layout;

public interface ISettingLayoutItem
{
    public void Draw(Configuration configuration, int? labelSize = null, int? inputSize = null);

    public void Build(IComponentContext context);
}
