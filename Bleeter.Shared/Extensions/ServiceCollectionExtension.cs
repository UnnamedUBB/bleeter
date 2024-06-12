using Bleeter.Shared.Data;
using Bleeter.Shared.Data.Interfaces;
using Bleeter.Shared.MessageBroker;
using Bleeter.Shared.MessageBroker.Interfaces;
using Bleeter.Shared.Middlewares;
using Bleeter.Shared.Pipelines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bleeter.Shared.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPersistence<TContext>(this IServiceCollection collection,
        string connectionString, string migrationsAssembly)
        where TContext : DbContext
    {
        collection.AddDbContext<TContext>(options =>
        {
            var version = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, version, x =>
            {
                x.MigrationsAssembly(migrationsAssembly);
            });
        });

        collection.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();

        return collection;
    }
    
    public static IServiceCollection AddMessageBroker(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.Configure<RabbitMqOptions>(configuration);
        collection.AddScoped(typeof(IRabbitMqPublisher<>), typeof(RabbitMqPublisher<>));
        
        return collection;
    }
    
    public static IServiceCollection AddMediator<T>(this IServiceCollection collection)
    {
        collection.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblyContaining<T>();
            x.AddOpenBehavior(typeof(ValidationPipeline<,>));
            x.AddOpenBehavior(typeof(TransactionPipeline<,>));
        });

        return collection;
    }

    public static IServiceCollection AddMiddlewares(this IServiceCollection collection)
    {
        collection.AddScoped<ExceptionMiddleware>();
        
        return collection;
    }
}