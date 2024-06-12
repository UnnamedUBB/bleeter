using System.Text;
using System.Text.Json;
using Bleeter.AccountService.MessageBroker.Interfaces;
using RabbitMQ.Client;

namespace Bleeter.AccountService.MessageBroker;

public class RabbitMqPublisher<T> : IRabbitMqPublisher<T>
{
    private readonly RabbitMqOptions _rabbitMqOptions;

    public RabbitMqPublisher(RabbitMqOptions rabbitMqOptions)
    {
        _rabbitMqOptions = rabbitMqOptions;
    }

    public async Task PublishMessageAsync(T message, string queueName)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMqOptions.HostName,
            UserName = _rabbitMqOptions.UserName,
            Password = _rabbitMqOptions.Password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queueName, false, false, false, null);

        var messageJson = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(messageJson);

        await Task.Run(() =>
        {
            channel.BasicPublish("", queueName, null, body);
        });
    }
}