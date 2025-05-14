using System;
using System.Collections.Generic;
using System.Text;
using DemoDatabaseCompare.Localization;
using Volo.Abp.Application.Services;

namespace DemoDatabaseCompare;

/* Inherit your application services from this class.
 */
public abstract class DemoDatabaseCompareAppService : ApplicationService
{
    protected DemoDatabaseCompareAppService()
    {
        LocalizationResource = typeof(DemoDatabaseCompareResource);
    }
}
