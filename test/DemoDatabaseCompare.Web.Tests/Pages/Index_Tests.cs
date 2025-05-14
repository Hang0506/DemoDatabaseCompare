using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace DemoDatabaseCompare.Pages;

public class Index_Tests : DemoDatabaseCompareWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
