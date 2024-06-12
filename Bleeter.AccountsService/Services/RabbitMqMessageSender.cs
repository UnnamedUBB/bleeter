using Bleeter.AccountService.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Bleeter.AccountService.Services;

public class RabbitMqMessageSender : IEmailSender<AccountModel>
{
    public Task SendConfirmationLinkAsync(AccountModel user, string email, string confirmationLink)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetLinkAsync(AccountModel user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetCodeAsync(AccountModel user, string email, string resetCode)
    {
        throw new NotImplementedException();
    }
}