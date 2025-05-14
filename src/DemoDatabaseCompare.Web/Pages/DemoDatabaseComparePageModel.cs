using DemoDatabaseCompare.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace DemoDatabaseCompare.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class DemoDatabaseComparePageModel : AbpPageModel
{
    protected DemoDatabaseComparePageModel()
    {
        LocalizationResourceType = typeof(DemoDatabaseCompareResource);
    }
}
