using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Microsoft.Extensions.DependencyInjection;
using DemoDatabaseCompare.Students; // or the correct namespace
using DemoCompare.Cassandra.Services;
using DemoCompare.Cassandra.Repositories;

namespace DemoDatabaseCompare;

[DependsOn(
    typeof(DemoDatabaseCompareDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(DemoDatabaseCompareApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class DemoDatabaseCompareApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<DemoDatabaseCompareApplicationModule>();
        });
        context.Services.AddTransient<IStudentCassandraService, StudentCassandraService>();
    }
}
