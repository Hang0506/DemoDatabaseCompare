using DemoDatabaseCompare.Samples;
using Xunit;

namespace DemoDatabaseCompare.EntityFrameworkCore.Domains;

[Collection(DemoDatabaseCompareTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<DemoDatabaseCompareEntityFrameworkCoreTestModule>
{

}
