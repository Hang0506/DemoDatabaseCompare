using Microsoft.AspNetCore.Builder;
using DemoDatabaseCompare;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("DemoDatabaseCompare.Web.csproj");
await builder.RunAbpModuleAsync<DemoDatabaseCompareWebTestModule>(applicationName: "DemoDatabaseCompare.Web" );

public partial class Program
{
}
