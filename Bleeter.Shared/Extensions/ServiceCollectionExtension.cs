using System.Text;
using Bleeter.Shared.Data;
using Bleeter.Shared.Data.Interfaces;
using Bleeter.Shared.Middlewares;
using Bleeter.Shared.Pipelines;
using Bleeter.Shared.Services;
using Bleeter.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Bleeter.Shared.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSharedServices(this IServiceCollection collection)
    {
        collection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        collection.AddScoped<IUserClaimService, UserClaimService>();
        
        return collection;
    }
    
    public static IServiceCollection AddJwtAuth(this IServiceCollection collection)
    {
        collection.AddAuthentication(opt =>
            {
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "http://localhost:5401",
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ktowcuroicmpyseqskuitzgyvgwmvxkx")),
                    ValidAudiences = new []{"http://localhost:5401", "http://localhost:5402"}
                };
            });

        return collection;
    } 
    
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