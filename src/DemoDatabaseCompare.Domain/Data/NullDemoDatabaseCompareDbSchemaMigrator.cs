using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace DemoDatabaseCompare.Data;

/* This is used if database provider does't define
 * IDemoDatabaseCompareDbSchemaMigrator implementation.
 */
public class NullDemoDatabaseCompareDbSchemaMigrator : IDemoDatabaseCompareDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
