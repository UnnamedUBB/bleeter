using Bleeter.AccountService.DAL;
using Bleeter.AccountService.DAL.Extensions;
using Bleeter.AccountService.DAL.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration.GetConnectionString("Default")!);

builder.Services.AddIdentityApiEndpoints<AccountModel>()
    .AddEntityFrameworkStores<AccountContext>();
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.MapIdentityApi<AccountModel>();

app.Run();