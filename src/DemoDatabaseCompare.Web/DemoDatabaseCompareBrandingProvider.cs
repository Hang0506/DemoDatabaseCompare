using Microsoft.Extensions.Localization;
using DemoDatabaseCompare.Localization;
using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace DemoDatabaseCompare.Web;

[Dependency(ReplaceServices = true)]
public class DemoDatabaseCompareBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<DemoDatabaseCompareResource> _localizer;

    public DemoDatabaseCompareBrandingProvider(IStringLocalizer<DemoDatabaseCompareResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
