using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Bleeter.Shared.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bleeter.Shared.Data;

public abstract class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var types = typeof(BaseDbContext).Assembly
            .GetTypes()
            .Where(x => x.GetCustomAttribute<TableAttribute>() is not null);
        
        foreach (var type in types)
        {
            var tableName = type.GetCustomAttribute<TableAttribute>()!.Name;
            var model = modelBuilder.Entity(type).ToTable(tableName);

            if (type.GetInterfaces().Any(x => x == typeof(IDeleted)))
            {
                model.HasQueryFilter((IDeleted x) => x.DataDeletedUtc != null);
            }
        }
    }
}