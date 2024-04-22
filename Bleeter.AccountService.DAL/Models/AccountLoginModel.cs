using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Bleeter.AccountService.DAL.Models;

[Table("AccountLogins")]
public class AccountLoginModel : IdentityUserLogin<Guid>
{
    public virtual AccountModel Account { get; set; }
}