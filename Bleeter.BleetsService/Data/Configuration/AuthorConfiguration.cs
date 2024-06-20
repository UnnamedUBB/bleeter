using Bleeter.BleetsService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bleeter.BleetsService.Data.Configuration;

public class AuthorConfiguration : IEntityTypeConfiguration<AuthorModel>
{
    public void Configure(EntityTypeBuilder<AuthorModel> builder)
    {
        builder
            .HasMany(x => x.Comments)
            .WithOne(x => x.Author)
            .HasForeignKey(x => x.AuthorId);

        builder
            .HasMany(x => x.Likes)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId);

        builder.HasQueryFilter(x => x.DataDeletedUtc == null);
    }
}