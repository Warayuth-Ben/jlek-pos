using JLek.POS.Domain.Payments;
using JLek.POS.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLek.POS.Infrastructure.Persistence.Configurations;

public sealed class PaymentConfiguration
    : IEntityTypeConfiguration<Payment>
{
    public void Configure(
        EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(new PaymentIdConverter())
               .ValueGeneratedNever();

        builder.Property(x => x.OrderId)
               .HasConversion(new OrderIdConverter());

        builder.OwnsOne(x => x.OrderTotal, money =>
        {
            money.Property(x => x.Amount)
                 .HasColumnName("OrderTotal");
        });

        builder.OwnsOne(x => x.AmountReceived, money =>
        {
            money.Property(x => x.Amount)
                 .HasColumnName("AmountReceived");
        });

        builder.OwnsOne(x => x.Change, money =>
        {
            money.Property(x => x.Amount)
                 .HasColumnName("Change");
        });

        builder.Property(x => x.Method)
               .HasConversion<int>();

        builder.Property(x => x.Status)
               .HasConversion<int>();

        builder.Property("_refundReason")
               .HasColumnName("RefundReason");

        builder.Ignore(x => x.DomainEvents);
    }
}