using Volo.Abp.Settings;

namespace DemoDatabaseCompare.Settings;

public class DemoDatabaseCompareSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(DemoDatabaseCompareSettings.MySetting1));
    }
}
