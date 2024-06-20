using System.Reflection;
using Bleeter.AccountsService.Data;
using Bleeter.AccountsService.Services;
using Bleeter.AccountsService.Utils;
using Bleeter.Shared.Extensions;
using Bleeter.Shared.Middlewares;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence<AccountContext>(builder.Configuration.GetConnectionString("Default")!,
    "Bleeter.AccountsService");

builder.Services.Configure<JwtSecurityTokenSettings>(builder.Configuration.GetSection("JwtSecurityTokenOptions") ?? throw new ArgumentNullException());

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMiddlewares();
builder.Services.AddMediator<Bleeter.AccountsService.Program>();
builder.Services.AddControllers();
builder.Services.AddSharedServices();

builder.Services.AddMassTransit(opt =>
{
    opt.SetKebabCaseEndpointNameFormatter();
    opt.UsingRabbitMq((ctx, mq) =>
    {
        var hostname = builder.Configuration.GetValue<string>("RabbitMq:HostName");
        var virtualHost = builder.Configuration.GetValue<string>("RabbitMq:VirtualHost");
        var username = builder.Configuration.GetValue<string>("RabbitMq:UserName");
        var password = builder.Configuration.GetValue<string>("RabbitMq:Password");
        
        mq.Host(hostname, virtualHost, (hostBuilder) =>
        {
            hostBuilder.Username(username!);
            hostBuilder.Password(password!);
        });
        mq.ConfigureEndpoints(ctx);
    });
});
builder.Services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
builder.Services.AddTransient<IEmailSender<IdentityUser<Guid>>, RabbitMqMessageSender>();

builder.Services.AddJwtAuth();
builder.Services.AddIdentityApiEndpoints<IdentityUser<Guid>>(x =>
    {
        x.Password.RequiredLength = 8;
    })
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AccountContext>();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.AllowAnyMethod();
    x.AllowAnyOrigin();
});

app.Run();

namespace Bleeter.AccountsService
{
    public partial class Program;
}