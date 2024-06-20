using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Bleeter.Tests.Shared;

public abstract class BaseIntegrationTest<TContext, TProgram> : IClassFixture<FunctionalTestWebAppFactory<TContext, TProgram>>, IDisposable
    where TContext : DbContext
    where TProgram : class
{
    protected readonly IServiceScope Scope;
    protected readonly TContext Context;
    protected readonly IMediator Mediator;
    
    protected BaseIntegrationTest(FunctionalTestWebAppFactory<TContext, TProgram> factory)
    {
        Scope= factory.Services.CreateScope();
        Context = Scope.ServiceProvider.GetRequiredService<TContext>();
        Mediator = Scope.ServiceProvider.GetRequiredService<IMediator>();
    }

    public void Dispose()
    {
        Context.Dispose();
        Scope.Dispose();
    }
}