using Bleeter.AccountService.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Bleeter.AccountService.Services;

public class RabbitMqMessageSender : IEmailSender<IdentityUser<Guid>>
{
    public Task SendConfirmationLinkAsync(IdentityUser<Guid> user, string email, string confirmationLink)
    {
        throw new NotImplementedException();
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