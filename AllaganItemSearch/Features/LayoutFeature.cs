using System.Collections.Generic;

using AllaganItemSearch.Settings;

using AllaganLib.Interface.FormFields;
using AllaganLib.Interface.Wizard;

namespace AllaganItemSearch.Features;

public class LayoutFeature(IEnumerable<IFormField<Configuration>> settings) : Feature<Configuration>(
    [
        typeof(FilterPanePositionSetting),
    ],
    settings)
{
    public override string Name { get; } = "Layout";

    public override string Description { get; } = "Customize the plugins layout.";
}
