using JLek.POS.Domain.Catalog;
using JLek.POS.Domain.Common.ValueObjects;
using JLek.POS.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLek.POS.Infrastructure.Persistence.Configurations;

public sealed class ProductConfiguration
    : IEntityTypeConfiguration<Product>
{
    public void Configure(
        EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(new ProductIdConverter())
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .IsRequired();

        builder.Property(x => x.Description);

        builder.Property(x => x.Status)
               .HasConversion<int>();

        builder.Property(x => x.Visibility)
               .HasConversion<int>();

        builder.Property(x => x.DisplayOrder);

        builder.Property(x => x.CategoryId)
               .HasConversion(new ProductCategoryIdConverter());

        builder.Ignore(x => x.DomainEvents);

        // OptionGroups — owned entity collection
        builder.HasMany(x => x.OptionGroups)
               .WithOne()
               .HasForeignKey("ProductId")
               .OnDelete(DeleteBehavior.Cascade);

        // Modifiers — owned entity collection
        builder.HasMany(x => x.Modifiers)
               .WithOne()
               .HasForeignKey("ProductId")
               .OnDelete(DeleteBehavior.Cascade);

        // SuggestedPrices — value object collection (backed by _suggestedPrices field)
        builder.OwnsMany<Money>("_suggestedPrices", price =>
        {
            price.WithOwner().HasForeignKey("ProductId");
            price.ToTable("ProductSuggestedPrices");
            price.Property<Guid>("ProductId");
            price.Property(x => x.Amount)
                 .HasColumnName("Amount")
                 .IsRequired();
        });

        // IngredientIds — value object collection (backed by _ingredientIds field)
        builder.OwnsMany<IngredientId>("_ingredientIds", ingredient =>
        {
            ingredient.WithOwner().HasForeignKey("ProductId");
            ingredient.ToTable("ProductIngredients");
            ingredient.Property<Guid>("ProductId");
            ingredient.Property(x => x.Value)
                      .HasColumnName("IngredientId")
                      .IsRequired();
        });
    }
}