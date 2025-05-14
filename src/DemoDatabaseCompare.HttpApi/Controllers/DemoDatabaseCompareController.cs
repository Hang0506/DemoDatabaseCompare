using DemoDatabaseCompare.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DemoDatabaseCompare.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class DemoDatabaseCompareController : AbpControllerBase
{
    protected DemoDatabaseCompareController()
    {
        LocalizationResource = typeof(DemoDatabaseCompareResource);
    }
}
