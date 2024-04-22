using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Bleeter.AccountService.DAL.Models;

[Table("AccountClaims")]
public class AccountClaimModel : IdentityUserClaim<Guid>
{
    public virtual AccountModel Account { get; set; }
}