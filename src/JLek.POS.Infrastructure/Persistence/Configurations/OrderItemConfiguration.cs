using JLek.POS.Domain.Orders;
using JLek.POS.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLek.POS.Infrastructure.Persistence.Configurations;

public sealed class OrderItemConfiguration
    : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(
        EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(new OrderItemIdConverter())
               .ValueGeneratedNever();

        builder.Property(x => x.MenuItemId);

        builder.Ignore(x => x.TotalPrice);

        builder.OwnsOne(x => x.Quantity, quantity =>
        {
            quantity.Property(x => x.Value)
                    .HasColumnName("Quantity");
        });

        builder.OwnsOne(x => x.UnitPrice, money =>
        {
            money.Property(x => x.Amount)
                 .HasColumnName("UnitPrice");
        });
    }
}