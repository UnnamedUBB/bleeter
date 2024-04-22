using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bleeter.AccountService.DAL.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection collection, string connectionString)
    {
        collection.AddDbContext<AccountContext>(options =>
        {
            var version = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, version, x =>
            {
                x.MigrationsAssembly("Bleeter.AccountService.DAL");
            });
        });
        
        return collection;
    } 
}