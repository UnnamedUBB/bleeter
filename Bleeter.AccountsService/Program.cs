using Bleeter.AccountService;
using Bleeter.AccountService.Data;
using Bleeter.AccountService.Data.Models;
using Bleeter.AccountService.Services;
using Bleeter.Shared.Extensions;
using Bleeter.Shared.MessageBroker;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMessageBroker(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddTransient<IEmailSender<AccountModel>, RabbitMqMessageSender>();
builder.Services.AddPersistence<AccountContext>(builder.Configuration.GetConnectionString("Default")!,
    "Bleeter.AccountsService");

builder.Services.AddIdentity<AccountModel, IdentityRole<Guid>>(x =>
    {
        x.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<AccountContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthorization();

builder.Services
    .AddIdentityServer()
    .AddAspNetIdentity<AccountModel>()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiResources(Config.GetApiResources())
    .AddInMemoryApiScopes(Config.ApiScopes())
    .AddInMemoryClients(Config.GetClients())
    .AddDeveloperSigningCredential();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();
app.MapIdentityApi<AccountModel>();
app.UseIdentityServer();

app.Run();