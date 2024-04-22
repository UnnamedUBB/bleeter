using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Bleeter.AccountService.DAL.Models;

[Table("AccountTokens")]
public class AccountTokenModel : IdentityUserToken<Guid>
{
    public virtual AccountModel Accounts { get; set; }
}