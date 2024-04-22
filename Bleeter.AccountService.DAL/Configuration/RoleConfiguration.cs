using Bleeter.AccountService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bleeter.AccountService.DAL.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<RoleModel>
{
    public void Configure(EntityTypeBuilder<RoleModel> builder)
    {
        builder.ToTable("Roles");
        
        // Each Role can have many entries in the UserRole join table
        builder.HasMany(e => e.AccountRole)
            .WithOne(e => e.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        // Each Role can have many associated RoleClaims
        builder.HasMany(e => e.Claims)
            .WithOne(e => e.Role)
            .HasForeignKey(rc => rc.RoleId)
            .IsRequired();
    }
}