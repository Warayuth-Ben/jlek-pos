using JLek.POS.Domain.Orders;

namespace JLek.POS.Application.Features.Orders.Responses;

public record OrderItemResponse(
    Guid MenuItemId,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice)
{
    public static OrderItemResponse FromDomain(OrderItem item)
    {
        return new OrderItemResponse(
            item.MenuItemId,
            item.Quantity.Value,
            item.UnitPrice.Amount,
            item.TotalPrice.Amount);
    }
}