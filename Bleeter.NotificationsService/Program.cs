using Bleeter.Notification;
using Bleeter.Notification.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(opt =>
{
    opt.AddConsumers(typeof(NotificationConsumer).Assembly);
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

var app = builder.Build();

app.Run();
