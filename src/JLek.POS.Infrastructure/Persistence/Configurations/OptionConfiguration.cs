using JLek.POS.Domain.Catalog;
using JLek.POS.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLek.POS.Infrastructure.Persistence.Configurations;

public sealed class OptionConfiguration
    : IEntityTypeConfiguration<Option>
{
    public void Configure(
        EntityTypeBuilder<Option> builder)
    {
        builder.ToTable("Options");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(new OptionIdConverter())
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .IsRequired();

        builder.Property(x => x.DisplayOrder);

        builder.Property(x => x.Status)
               .HasConversion<int>();

        builder.Property(x => x.Visibility)
               .HasConversion<int>();

        builder.Property(x => x.PriceAdjustment);
    }
}