using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Common.ValueObjects;
using JLek.POS.Domain.Orders.Events;
using JLek.POS.Domain.Orders.Rules;
using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Domain.Orders;

public sealed class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = [];

    private Order(OrderId id)
        : base(id)
    {
        Status = OrderStatus.Draft;
    }

    public OrderStatus Status { get; private set; }

    public IReadOnlyCollection<OrderItem> Items =>
        _items.AsReadOnly();

    public Money Total =>
        _items.Aggregate(
            Money.Zero,
            (total, item) => total + item.TotalPrice);

    public static Order Create()
    {
        var order = new Order(OrderId.New());

        order.RaiseDomainEvent(
            new OrderCreatedEvent(order.Id));

        return order;
    }

    public void AddItem(
        Guid menuItemId,
        Quantity quantity,
        Money unitPrice)
    {
        CheckRule(
            new CannotAddItemToCompletedOrderRule(Status));

        var item = OrderItem.Create(
            menuItemId,
            quantity,
            unitPrice);

        _items.Add(item);
    }

    public void Confirm()
    {
        CheckRule(
            new CannotConfirmEmptyOrderRule(_items.Count));

        Status = OrderStatus.Confirmed;

        RaiseDomainEvent(
            new OrderConfirmedEvent(Id));
    }

    public void Complete()
    {
        CheckRule(
            new CannotCompleteDraftOrderRule(Status));

        Status = OrderStatus.Completed;

        RaiseDomainEvent(
            new OrderCompletedEvent(Id));
    }
}
