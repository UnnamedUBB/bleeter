using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bleeter.Shared.DAL.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence<TContext>(this IServiceCollection collection,
        string connectionString, string migrationsAssembly)
        where TContext : BaseDbContext
    {
        collection.AddDbContext<TContext>(options =>
        {
            var version = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, version, x =>
            {
                x.MigrationsAssembly(migrationsAssembly);
            });
        });

        return collection;
    }
}