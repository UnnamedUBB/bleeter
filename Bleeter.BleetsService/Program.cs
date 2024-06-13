using Bleeter.BleetsService.Data;
using Bleeter.BleetsService.Data.Repositories;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.Shared.Extensions;
using Bleeter.Shared.Middlewares;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediator<Program>();
builder.Services.AddMiddlewares();
builder.Services.AddPersistence<BleetsContext>(
    builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException()
    , "Bleeter.BleetsService");

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBleetRepository, BleetRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", x =>
    {
        x.Authority = builder.Configuration.GetValue<string>("IdentityServerHostName");
        x.ApiName = "secretApi";
        x.RequireHttpsMetadata = false;
    });
    // .AddJwtBearer("Bearer", x =>
    // {
    //     x.Authority = builder.Configuration.GetValue<string>("IdentityServerHostName");
    //     x.Audience = "secretApi";
    //     x.RequireHttpsMetadata = false;
    // });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.AllowAnyMethod();
    x.AllowAnyOrigin();
});

app.Run();