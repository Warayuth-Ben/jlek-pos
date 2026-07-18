using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.Common.ValueObjects;
using JLek.POS.Domain.Orders;

namespace JLek.POS.Application.Features.Orders.Responses;

public record OrderResponseV2(
    OrderId Id,
    OrderStatus Status,
    Money Total,
    IEnumerable<OrderItemResponse> Items)
{
    public static OrderResponseV2 FromDomain(Order order)
    {
        return new OrderResponseV2(
            order.Id,
            order.Status,
            order.Total,
            order.Items.Select(item => OrderItemResponse.FromDomain(item)));
    }
}
