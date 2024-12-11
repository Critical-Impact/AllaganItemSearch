using System.Collections.Generic;

using AllaganItemSearch.Settings;

using AllaganLib.Interface.FormFields;
using AllaganLib.Interface.Wizard;

namespace AllaganItemSearch.Features;

public class MiscFeature(IEnumerable<IFormField<Configuration>> settings) : Feature<Configuration>(
    [
        typeof(AddTitleMenuButtonSetting),
    ],
    settings)
{
    public override string Name { get; } = "Misc Features";

    public override string Description { get; } = "Miscellaneous features that improve your overall experience.";
}
