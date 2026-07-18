using JLek.POS.Domain.Orders;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.Common.ValueObjects;

namespace JLek.POS.Application.Features.Orders.Responses;

public record OrderItemResponse(
    Guid MenuItemId,
    Quantity Quantity,
    Money UnitPrice,
    Money TotalPrice)
{
    public static OrderItemResponse FromDomain(OrderItem item)
    {
        return new OrderItemResponse(
            item.MenuItemId,
            item.Quantity,
            item.UnitPrice,
            item.TotalPrice);
    }
}