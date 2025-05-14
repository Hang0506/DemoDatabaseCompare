using DemoDatabaseCompare.Samples;
using Xunit;

namespace DemoDatabaseCompare.EntityFrameworkCore.Applications;

[Collection(DemoDatabaseCompareTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<DemoDatabaseCompareEntityFrameworkCoreTestModule>
{

}
