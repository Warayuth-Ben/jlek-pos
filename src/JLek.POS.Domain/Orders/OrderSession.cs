using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Domain.Orders;

public sealed class OrderSession : AggregateRoot<OrderSessionId>
{
    private readonly List<Order> _orders = [];

    private OrderSession(OrderSessionId id)
        : base(id)
    {
    }

    public IReadOnlyCollection<Order> Orders =>
        _orders.AsReadOnly();

    public static OrderSession Create()
    {
        return new OrderSession(
            OrderSessionId.New());
    }
}
