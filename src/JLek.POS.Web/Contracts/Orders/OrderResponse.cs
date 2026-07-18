namespace JLek.POS.Web.Contracts.Orders;

public sealed record OrderItemResponse(
    Guid Id,
    Guid MenuItemId,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice);

public sealed record OrderResponse(
    Guid Id,
    Guid? TableId,
    string Status,
    decimal Total,
    List<OrderItemResponse> Items);