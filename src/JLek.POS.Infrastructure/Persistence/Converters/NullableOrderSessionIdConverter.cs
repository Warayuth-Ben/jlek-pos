using JLek.POS.Domain.Orders.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class NullableOrderSessionIdConverter
    : ValueConverter<OrderSessionId?, Guid?>
{
    public NullableOrderSessionIdConverter()
        : base(
            v => v == null ? null : v.Value,
            v => v == null ? null : new OrderSessionId(v.Value))
    {
    }
}