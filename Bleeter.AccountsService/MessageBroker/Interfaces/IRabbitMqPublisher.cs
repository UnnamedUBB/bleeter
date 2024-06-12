namespace Bleeter.AccountService.MessageBroker.Interfaces;

public interface IRabbitMqPublisher<T>
{
    Task PublishMessageAsync(T message, string queueName);
}