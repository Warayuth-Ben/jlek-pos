using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Common.ValueObjects;
using JLek.POS.Domain.Orders.Events;
using JLek.POS.Domain.Orders.Rules;
using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Domain.Orders;

public sealed class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = [];

    // Constructor สำหรับ EF Core
    private Order()
        : base(OrderId.From(Guid.Empty))
    {
        Status = OrderStatus.Draft;
    }

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
        CheckRule(new CannotModifyConfirmedOrderRule(Status));
        CheckRule(new CannotModifyCancelledOrderRule(Status));

        _items.Add(
            OrderItem.Create(
                menuItemId,
                quantity,
                unitPrice));
    }

    public void RemoveItem(OrderItemId itemId)
    {
        CheckRule(new CannotModifyConfirmedOrderRule(Status));
        CheckRule(new CannotModifyCancelledOrderRule(Status));

        var item = _items.FirstOrDefault(x => x.Id == itemId);

        if (item is not null)
        {
            _items.Remove(item);
        }
    }

    public void ChangeItemQuantity(
        OrderItemId itemId,
        Quantity quantity)
    {
        CheckRule(new CannotModifyConfirmedOrderRule(Status));
        CheckRule(new CannotModifyCancelledOrderRule(Status));

        var item = _items.FirstOrDefault(x => x.Id == itemId);

        item?.ChangeQuantity(quantity);
    }

    public void Confirm()
    {
        CheckRule(new CannotConfirmNonDraftOrderRule(Status));
        CheckRule(new CannotConfirmEmptyOrderRule(_items.Count));

        Status = OrderStatus.Confirmed;

        RaiseDomainEvent(new OrderConfirmedEvent(Id));
    }

    public void Complete()
    {
        CheckRule(new CannotCompleteNonConfirmedOrderRule(Status));

        Status = OrderStatus.Completed;

        RaiseDomainEvent(new OrderCompletedEvent(Id));
    }
}