namespace JLek.POS.Application.Features.Orders.Responses;

public record OrderResponseV2(
    Guid Id,
    string Status,
    decimal Total,
    IEnumerable<OrderItemResponse> Items)
{
    public static OrderResponseV2 FromDomain(
        JLek.POS.Domain.Orders.Order order)
    {
        return new OrderResponseV2(
            order.Id.Value,
            order.Status.ToString(),
            order.Total.Amount,
            order.Items.Select(item => OrderItemResponse.FromDomain(item)));
    }
}