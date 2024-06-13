using Bleeter.BleetsService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bleeter.BleetsService.Data.Configuration;

public class LikeConfiguration : IEntityTypeConfiguration<LikeModel>
{
    public void Configure(EntityTypeBuilder<LikeModel> builder)
    {
        builder.HasQueryFilter(x => x.DataDeletedUtc == null);
    }
}