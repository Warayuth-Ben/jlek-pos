using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Common.ValueObjects;
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

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Money Total =>
        _items.Aggregate(
            Money.Zero,
            (total, item) => total + item.TotalPrice);

    public static Order Create()
    {
        return new(OrderId.New());
    }

    public void AddItem(
        Guid menuItemId,
        Quantity quantity,
        Money unitPrice)
    {
        var item = OrderItem.Create(
            menuItemId,
            quantity,
            unitPrice);

        _items.Add(item);
    }

    public void Confirm()
    {
        Status = OrderStatus.Confirmed;
    }

    public void Complete()
    {
        Status = OrderStatus.Completed;
    }
}
