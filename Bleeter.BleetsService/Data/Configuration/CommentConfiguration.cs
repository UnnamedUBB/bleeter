using Bleeter.BleetsService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bleeter.BleetsService.Data.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<CommentModel>
{
    public void Configure(EntityTypeBuilder<CommentModel> builder)
    {
        builder.HasQueryFilter(x => x.DataDeletedUtc == null);
    }
}