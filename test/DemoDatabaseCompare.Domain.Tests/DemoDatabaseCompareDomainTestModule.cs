using Volo.Abp.Modularity;

namespace DemoDatabaseCompare;

[DependsOn(
    typeof(DemoDatabaseCompareDomainModule),
    typeof(DemoDatabaseCompareTestBaseModule)
)]
public class DemoDatabaseCompareDomainTestModule : AbpModule
{

}
