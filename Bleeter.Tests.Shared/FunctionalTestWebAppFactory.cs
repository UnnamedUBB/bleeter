using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MariaDb;
using Xunit;

namespace Bleeter.Tests.Shared;

public class FunctionalTestWebAppFactory<TContext, TEntryPoint> : WebApplicationFactory<TEntryPoint>, IAsyncLifetime
    where TContext : DbContext
    where TEntryPoint : class
{
    private readonly MariaDbContainer _dbContainer = new MariaDbBuilder()
        .WithImage("mariadb")
        .WithUsername("root")
        .WithPassword("root")
        .Build();
    
    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(x =>
        {
            var serviceDescriptor = typeof(TContext);
            if (x.FirstOrDefault(t => t.ServiceType == serviceDescriptor) is {} descriptor)
            {
                x.Remove(descriptor);
            }

            x.AddDbContext<TContext>(t =>
            {
                var version = ServerVersion.AutoDetect(_dbContainer.GetConnectionString());
                t.UseMySql(_dbContainer.GetConnectionString(), version);
            });
        });
    }
}