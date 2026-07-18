using JLek.POS.Domain.Orders.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class OrderSessionIdConverter
    : ValueConverter<OrderSessionId, Guid>
{
    public OrderSessionIdConverter()
        : base(
            id => id.Value,
            value => new OrderSessionId(value))
    {
    }
}