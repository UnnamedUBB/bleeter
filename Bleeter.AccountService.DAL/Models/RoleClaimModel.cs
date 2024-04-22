using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Bleeter.AccountService.DAL.Models;

[Table("RoleClaims")]
public class RoleClaimModel : IdentityRoleClaim<Guid>
{
    public virtual RoleModel Role { get; set; }
}