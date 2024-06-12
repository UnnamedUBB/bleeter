using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Bleeter.AccountService.Data.Models;

public class AccountModel : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}