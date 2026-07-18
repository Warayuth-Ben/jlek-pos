using JLek.POS.Domain.Tables;
using JLek.POS.Domain.ValueObjects;
using JLek.POS.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JLek.POS.Infrastructure.Persistence.Configurations;

public sealed class DiningTableConfiguration
    : IEntityTypeConfiguration<DiningTable>
{
    public void Configure(
        EntityTypeBuilder<DiningTable> builder)
    {
        builder.ToTable("DiningTables");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(new TableIdConverter())
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .IsRequired();

        builder.Property(x => x.Status)
               .HasConversion<int>();

        builder.Property(x => x.ActiveSessionId)
               .HasConversion(new NullableOrderSessionIdConverter());

        // MergedTableIds — value object collection (backed by _mergedTableIds field)
        builder.OwnsMany<TableId>("_mergedTableIds", merged =>
        {
            merged.WithOwner().HasForeignKey("DiningTableId");
            merged.ToTable("DiningTableMergedTables");
            merged.Property<TableId>("DiningTableId")
                  .HasConversion(new TableIdConverter());
            merged.Property(x => x.Value)
                  .HasColumnName("MergedTableId")
                  .IsRequired();
        });

        builder.Ignore(x => x.MergedTableIds);
        builder.Ignore(x => x.DomainEvents);
    }
}