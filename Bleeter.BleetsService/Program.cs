using Bleeter.BleetsService.Data;
using Bleeter.BleetsService.Data.Repositories;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.BleetsService.Services;
using Bleeter.BleetsService.Services.Interfaces;
using Bleeter.Shared.Extensions;
using Bleeter.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence<BleetsContext>(
    builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException()
    , "Bleeter.BleetsService");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediator<Program>();
builder.Services.AddMiddlewares();
builder.Services.AddSharedServices();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBleetRepository, BleetRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBleetService, BleetService>();

builder.Services.AddJwtAuth();
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