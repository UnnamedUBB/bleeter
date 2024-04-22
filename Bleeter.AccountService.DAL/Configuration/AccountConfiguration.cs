using Bleeter.AccountService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bleeter.AccountService.DAL.Configuration;

public class AccountConfiguration : IEntityTypeConfiguration<AccountModel>
{
    public void Configure(EntityTypeBuilder<AccountModel> builder)
    {
        builder.ToTable("Accounts");
        
        // Each User can have many UserClaims
        builder.HasMany(e => e.Claims)
            .WithOne(e => e.Account)
            .HasForeignKey(uc => uc.UserId)
            .IsRequired();

        // Each User can have many UserLogins
        builder.HasMany(e => e.Logins)
            .WithOne(e => e.Account)
            .HasForeignKey(ul => ul.UserId)
            .IsRequired();

        // Each User can have many UserTokens
        builder.HasMany(e => e.Tokens)
            .WithOne(e => e.Accounts)
            .HasForeignKey(ut => ut.UserId)
            .IsRequired();

        // Each User can have many entries in the UserRole join table
        builder.HasMany(e => e.Roles)
            .WithOne(e => e.Account)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
    }
}