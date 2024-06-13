using Bleeter.Shared.Messages;
using MassTransit;

namespace Bleeter.Notification.Consumers;

public class NotificationConsumer : IConsumer<EmailConfirmationMessage>
{
    public Task Consume(ConsumeContext<EmailConfirmationMessage> context)
    {
        Console.WriteLine($"Przyszła wiadomość, którą należy wysłać do {context.Message.Email} z linkiem {context.Message.ConfirmationLink}");
        
        return Task.CompletedTask;
    }
}