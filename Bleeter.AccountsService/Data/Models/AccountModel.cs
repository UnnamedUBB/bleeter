using Microsoft.AspNetCore.Identity;

namespace Bleeter.AccountsService.Data.Models;

public class AccountModel : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}