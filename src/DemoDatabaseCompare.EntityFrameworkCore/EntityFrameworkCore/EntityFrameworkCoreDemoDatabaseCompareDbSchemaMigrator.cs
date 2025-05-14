using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DemoDatabaseCompare.Data;
using Volo.Abp.DependencyInjection;

namespace DemoDatabaseCompare.EntityFrameworkCore;

public class EntityFrameworkCoreDemoDatabaseCompareDbSchemaMigrator
    : IDemoDatabaseCompareDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreDemoDatabaseCompareDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the DemoDatabaseCompareDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<DemoDatabaseCompareDbContext>()
            .Database
            .MigrateAsync();
    }
}
