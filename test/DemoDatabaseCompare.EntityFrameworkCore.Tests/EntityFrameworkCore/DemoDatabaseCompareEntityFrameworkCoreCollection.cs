﻿using Xunit;

namespace DemoDatabaseCompare.EntityFrameworkCore;

[CollectionDefinition(DemoDatabaseCompareTestConsts.CollectionDefinitionName)]
public class DemoDatabaseCompareEntityFrameworkCoreCollection : ICollectionFixture<DemoDatabaseCompareEntityFrameworkCoreFixture>
{

}
