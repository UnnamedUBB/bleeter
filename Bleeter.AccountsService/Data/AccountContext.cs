﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bleeter.AccountsService.Data;

public class AccountContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
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