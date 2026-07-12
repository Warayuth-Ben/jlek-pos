using JLek.POS.Domain.Orders;

namespace JLek.POS.Api.Responses;

public static class OrderResponseMappings
{
    public static OrderResponse ToResponse(this Order order)
    {
        return new(
            order.Id.Value,
            order.Status.ToString());
    }
}
