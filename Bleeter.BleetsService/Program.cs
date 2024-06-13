using System.Reflection;
using Bleeter.BleetsService.Data;
using Bleeter.BleetsService.Data.Repositories;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.BleetsService.Services;
using Bleeter.BleetsService.Services.Interfaces;
using Bleeter.Shared.Extensions;
using Bleeter.Shared.Middlewares;
using MassTransit;

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

builder.Services.AddMassTransit(opt =>
{
    opt.AddConsumers(Assembly.GetExecutingAssembly());
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