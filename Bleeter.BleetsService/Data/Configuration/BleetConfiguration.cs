using Bleeter.BleetsService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bleeter.BleetsService.Data.Configuration;

public class BleetConfiguration : IEntityTypeConfiguration<BleetModel>
{
    public void Configure(EntityTypeBuilder<BleetModel> builder)
    {
        builder
            .HasMany(x => x.Likes)
            .WithOne(x => x.Bleet)
            .HasForeignKey(x => x.BleetId);

        builder
            .HasMany(x => x.Comments)
            .WithOne(x => x.Bleet)
            .HasForeignKey(x => x.BleetId);
    }
}