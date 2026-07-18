using JLek.POS.Domain.Catalog;
using JLek.POS.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLek.POS.Infrastructure.Persistence.Configurations;

public sealed class ModifierConfiguration
    : IEntityTypeConfiguration<Modifier>
{
    public void Configure(
        EntityTypeBuilder<Modifier> builder)
    {
        builder.ToTable("Modifiers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(new ModifierIdConverter())
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .IsRequired();

        builder.Property(x => x.Status)
               .HasConversion<int>();

        builder.Property(x => x.Visibility)
               .HasConversion<int>();

        builder.Property(x => x.PriceAdjustment);
    }
}