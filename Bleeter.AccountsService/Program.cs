using Bleeter.AccountService.Data;
using Bleeter.AccountService.Services;
using Bleeter.AccountService.Utils;
using Bleeter.Shared.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence<AccountContext>(builder.Configuration.GetConnectionString("Default")!,
    "Bleeter.AccountsService");

builder.Services.Configure<JwtSecurityTokenSettings>(builder.Configuration.GetSection("JwtSecurityTokenOptions") ?? throw new ArgumentNullException());
builder.Services.AddMediator<Program>();
builder.Services.AddMiddlewares();
builder.Services.AddControllers();

builder.Services.AddMessageBroker(builder.Configuration.GetSection("RabbitMq"));
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

app.MapControllers();
app.MapIdentityApi<IdentityUser<Guid>>();

app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.AllowAnyMethod();
    x.AllowAnyOrigin();
});

app.Run();