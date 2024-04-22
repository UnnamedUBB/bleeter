using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Bleeter.AccountService.DAL.Models;

[Table("Roles")]
public class RoleModel : IdentityRole<Guid>
{
    public virtual ICollection<AccountRoleModel> AccountRole { get; set; }
    public virtual ICollection<RoleClaimModel> Claims { get; set; }
}