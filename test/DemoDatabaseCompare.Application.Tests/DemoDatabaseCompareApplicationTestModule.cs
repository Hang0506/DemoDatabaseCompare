using Volo.Abp.Modularity;

namespace DemoDatabaseCompare;

[DependsOn(
    typeof(DemoDatabaseCompareApplicationModule),
    typeof(DemoDatabaseCompareDomainTestModule)
)]
public class DemoDatabaseCompareApplicationTestModule : AbpModule
{

}
