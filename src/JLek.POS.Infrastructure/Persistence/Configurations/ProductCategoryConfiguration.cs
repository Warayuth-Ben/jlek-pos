using JLek.POS.Domain.Catalog;
using JLek.POS.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLek.POS.Infrastructure.Persistence.Configurations;

public sealed class ProductCategoryConfiguration
    : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(
        EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("ProductCategories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(new ProductCategoryIdConverter())
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .IsRequired();

        builder.Property(x => x.DisplayOrder);

        builder.Property(x => x.Status)
               .HasConversion<int>();

        builder.Ignore(x => x.DomainEvents);
    }
}