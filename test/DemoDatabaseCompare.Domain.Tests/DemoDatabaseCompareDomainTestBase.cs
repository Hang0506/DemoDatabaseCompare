using Volo.Abp.Modularity;

namespace DemoDatabaseCompare;

/* Inherit from this class for your domain layer tests. */
public abstract class DemoDatabaseCompareDomainTestBase<TStartupModule> : DemoDatabaseCompareTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
