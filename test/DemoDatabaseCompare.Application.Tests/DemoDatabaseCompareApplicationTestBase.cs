using Volo.Abp.Modularity;

namespace DemoDatabaseCompare;

public abstract class DemoDatabaseCompareApplicationTestBase<TStartupModule> : DemoDatabaseCompareTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
