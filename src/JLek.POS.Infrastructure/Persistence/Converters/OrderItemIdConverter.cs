using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Infrastructure.Persistence.Converters;

public sealed class OrderItemIdConverter
    : StronglyTypedIdConverter<OrderItemId>
{
    public OrderItemIdConverter()
        : base(
            id => id.Value,
            OrderItemId.From)
    {
    }
}