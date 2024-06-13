using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Bleeter.Shared.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bleeter.Shared.Data;

public abstract class BaseDbContext<TContext> : DbContext
    where TContext : DbContext
{
    public BaseDbContext(DbContextOptions<TContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var types = typeof(TContext).Assembly
            .GetTypes()
            .Where(x => x.GetCustomAttribute<TableAttribute>() is not null);
        
        foreach (var type in types)
        {
            var tableName = type.GetCustomAttribute<TableAttribute>()!.Name;
            modelBuilder.Entity(type).ToTable(tableName);
        }
    }
}