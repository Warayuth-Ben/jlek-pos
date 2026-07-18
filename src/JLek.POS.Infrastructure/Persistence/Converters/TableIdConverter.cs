using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class TableIdConverter
    : StronglyTypedIdConverter<TableId>
{
    public TableIdConverter()
        : base(
            id => id.Value,
            TableId.From)
    {
    }
}