using JLek.POS.Domain.Orders;
using JLek.POS.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLek.POS.Infrastructure.Persistence.Configurations;

public sealed class OrderConfiguration
    : IEntityTypeConfiguration<Order>
{
    public void Configure(
        EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(new OrderIdConverter())
               .ValueGeneratedNever();

        builder.Property(x => x.Status)
               .HasConversion<int>();

        builder.Property(x => x.TableId)
               .HasConversion(new TableIdConverter());

        builder.Property(x => x.SessionId)
               .HasConversion(new OrderSessionIdConverter());

        builder.Ignore(x => x.DomainEvents);
        builder.Ignore(x => x.Total);

        builder.HasMany(x => x.Items)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);
    }
}