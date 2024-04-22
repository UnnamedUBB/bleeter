using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Bleeter.AccountService.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bleeter.AccountService.DAL;

public class AccountContext : IdentityDbContext<AccountModel, RoleModel, Guid, AccountClaimModel, AccountRoleModel,
    AccountLoginModel, RoleClaimModel, AccountTokenModel>
{
    public AccountContext(DbContextOptions<AccountContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var assembly = typeof(AccountContext).Assembly;

        var models = assembly
            .GetTypes()
            .Where(x => x.GetCustomAttribute<TableAttribute>() is not null)
            .ToList();

        foreach (var model in models)
        {
            var tableName = model.GetCustomAttribute<TableAttribute>()!.Name;
            modelBuilder.Entity(model).ToTable(tableName);
        }

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}