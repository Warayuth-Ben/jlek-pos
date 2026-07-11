using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Common.ValueObjects;
using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Domain.Orders;

public sealed class OrderItem : Entity<OrderItemId>
{
    // Constructor สำหรับ EF Core
    private OrderItem()
        : base(OrderItemId.From(Guid.Empty))
    {
        MenuItemId = Guid.Empty;
        Quantity = Quantity.Zero;
        UnitPrice = Money.Zero;
    }

    private OrderItem(
        OrderItemId id,
        Guid menuItemId,
        Quantity quantity,
        Money unitPrice)
        : base(id)
    {
        MenuItemId = menuItemId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public Guid MenuItemId { get; private set; }

    public Quantity Quantity { get; private set; }

    public Money UnitPrice { get; private set; }

    public Money TotalPrice =>
        UnitPrice * Quantity.Value;

    public static OrderItem Create(
        Guid menuItemId,
        Quantity quantity,
        Money unitPrice)
    {
        return new(
            OrderItemId.New(),
            menuItemId,
            quantity,
            unitPrice);
    }

    public void ChangeQuantity(Quantity quantity)
    {
        if (Quantity == quantity)
        {
            return;
        }

        Quantity = quantity;
    }

    public void ChangeUnitPrice(Money unitPrice)
    {
        if (UnitPrice == unitPrice)
        {
            return;
        }

        UnitPrice = unitPrice;
    }
}