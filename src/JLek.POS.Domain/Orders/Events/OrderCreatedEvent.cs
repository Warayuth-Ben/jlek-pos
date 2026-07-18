using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Domain.Orders.Events;

public sealed class OrderCreatedEvent : DomainEvent
{
    public OrderCreatedEvent(
        OrderId orderId,
        TableId tableId)
    {
        OrderId = orderId;
        TableId = tableId;
    }

    public OrderId OrderId { get; }

    public TableId TableId { get; }
}
