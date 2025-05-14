using DemoDatabaseCompare.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace DemoDatabaseCompare.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DemoDatabaseCompareEntityFrameworkCoreModule),
    typeof(DemoDatabaseCompareApplicationContractsModule)
    )]
public class DemoDatabaseCompareDbMigratorModule : AbpModule
{
}
