using Bleeter.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Bleeter.AccountsService.Services;

public class RabbitMqMessageSender : IEmailSender<IdentityUser<Guid>>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitMqMessageSender(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task SendConfirmationLinkAsync(IdentityUser<Guid> user, string email, string confirmationLink)
    {
        _publishEndpoint.Publish(new EmailConfirmationMessage()
        {
            Email = email,
            ConfirmationLink = confirmationLink,
        });
        
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(IdentityUser<Guid> user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetCodeAsync(IdentityUser<Guid> user, string email, string resetCode)
    {
        throw new NotImplementedException();
    }
}