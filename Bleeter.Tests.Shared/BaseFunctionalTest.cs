using Microsoft.EntityFrameworkCore;

namespace Bleeter.Tests.Shared;

public abstract class BaseFunctionalTest<TContext, TEntryPoint> : BaseIntegrationTest<TContext, TEntryPoint>, IDisposable
    where TContext : DbContext
    where TEntryPoint : class
{
    protected readonly HttpClient HttpClient;
    
    public BaseFunctionalTest(FunctionalTestWebAppFactory<TContext, TEntryPoint> factory) : base(factory)
    {
        HttpClient = factory.CreateClient();
    }

    public new void Dispose()
    {
        HttpClient.Dispose();
        base.Dispose();
    }
}