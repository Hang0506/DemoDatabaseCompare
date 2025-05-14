using DemoDatabaseCompare.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DemoDatabaseCompare.Permissions;

public class DemoDatabaseComparePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DemoDatabaseComparePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(DemoDatabaseComparePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DemoDatabaseCompareResource>(name);
    }
}
