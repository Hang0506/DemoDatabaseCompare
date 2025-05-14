using System.Threading.Tasks;

namespace DemoDatabaseCompare.Data;

public interface IDemoDatabaseCompareDbSchemaMigrator
{
    Task MigrateAsync();
}
