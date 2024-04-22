using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Bleeter.AccountService.DAL.Models;

[Table("AccountRoles")]
public class AccountRoleModel : IdentityUserRole<Guid>
{
    public virtual AccountModel Account { get; set; }
    public virtual RoleModel Role { get; set; }
}