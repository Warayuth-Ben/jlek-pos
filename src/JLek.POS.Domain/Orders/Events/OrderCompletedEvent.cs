using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Domain.Orders.Events;

public sealed class OrderCompletedEvent : DomainEvent
{
    public OrderCompletedEvent(OrderId orderId)
    {
        OrderId = orderId;
    }

    public OrderId OrderId { get; }
}
