using JLek.POS.Domain.Kitchen;
using JLek.POS.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLek.POS.Infrastructure.Persistence.Configurations;

public sealed class KitchenTicketConfiguration
    : IEntityTypeConfiguration<KitchenTicket>
{
    public void Configure(
        EntityTypeBuilder<KitchenTicket> builder)
    {
        builder.ToTable("KitchenTickets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(new KitchenTicketIdConverter())
               .ValueGeneratedNever();

        builder.Property(x => x.TicketNumber)
               .IsRequired();

        builder.Property(x => x.Status)
               .HasConversion<int>();

        // KitchenItems — owned entity collection (backed by _items field)
        builder.OwnsMany<KitchenItem>("_items", item =>
        {
            item.WithOwner().HasForeignKey("KitchenTicketId");
            item.ToTable("KitchenItems");
            item.HasKey(x => x.Id);

            item.Property(x => x.Id)
                .HasConversion(new KitchenItemIdConverter())
                .ValueGeneratedNever();

            item.Property(x => x.ItemName)
                .IsRequired()
                .HasMaxLength(200);

            item.Property(x => x.Quantity)
                .IsRequired();

            item.Property(x => x.Notes)
                .HasMaxLength(500);
        });

        builder.Ignore(x => x.DomainEvents);
    }
}