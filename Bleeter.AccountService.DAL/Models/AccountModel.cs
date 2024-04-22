using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Bleeter.AccountService.DAL.Models;

[Table("Accounts")]
public class AccountModel : IdentityUser<Guid>
{
    public virtual ICollection<AccountClaimModel> Claims { get; set; }
    public virtual ICollection<AccountLoginModel> Logins { get; set; }
    public virtual ICollection<AccountTokenModel> Tokens { get; set; }
    public virtual ICollection<AccountRoleModel> Roles { get; set; }
}