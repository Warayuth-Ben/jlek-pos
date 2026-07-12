namespace JLek.POS.Api.Requests;

public sealed record AddItemRequest(
    Guid MenuItemId,
    int Quantity,
    decimal UnitPrice);
