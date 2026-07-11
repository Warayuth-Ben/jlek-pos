using JLek.POS.Domain.Orders.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class OrderIdConverter
    : StronglyTypedIdConverter<OrderId>
{
    public OrderIdConverter()
        : base(
            id => id.Value,
            OrderId.From)
    {
    }
}