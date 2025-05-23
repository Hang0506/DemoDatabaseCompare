﻿using System.Threading.Tasks;
using DemoDatabaseCompare.Localization;
using DemoDatabaseCompare.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace DemoDatabaseCompare.Web.Menus;

public class DemoDatabaseCompareMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<DemoDatabaseCompareResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                DemoDatabaseCompareMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        context.Menu.Items.Insert(
            1,
            new ApplicationMenuItem(
                DemoDatabaseCompareMenus.Demo,
                l["Menu:Demo"],
                "/Students",
                icon: "fas fa-laptop-code",
                order: 1
            )
        );

        context.Menu.Items.Insert(
            2,
            new ApplicationMenuItem(
                DemoDatabaseCompareMenus.FAQ,
                l["Menu:FAQ"],
                "/FAQ",
                icon: "fas fa-question-circle",
                order: 2
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}
