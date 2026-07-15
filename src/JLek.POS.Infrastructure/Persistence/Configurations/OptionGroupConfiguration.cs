using JLek.POS.Domain.Catalog;
using JLek.POS.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLek.POS.Infrastructure.Persistence.Configurations;

public sealed class OptionGroupConfiguration
    : IEntityTypeConfiguration<OptionGroup>
{
    public void Configure(
        EntityTypeBuilder<OptionGroup> builder)
    {
        builder.ToTable("OptionGroups");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(new OptionGroupIdConverter())
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .IsRequired();

        builder.Property(x => x.DisplayOrder);

        builder.Property(x => x.Status)
               .HasConversion<int>();

        builder.Property(x => x.Visibility)
               .HasConversion<int>();

        // SelectionRule — inline columns (value object)
        builder.OwnsOne(x => x.Rule, rule =>
        {
            rule.Property(x => x.Min)
                .HasColumnName("MinSelection");

            rule.Property(x => x.Max)
                .HasColumnName("MaxSelection");
        });

        // Options — owned entity collection
        builder.HasMany(x => x.Options)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
    }
}