namespace JLek.POS.Web.Contracts.Kitchen;

public sealed record KitchenItemResponse(
    Guid Id,
    string ItemName,
    int Quantity,
    string? Notes);

public sealed record KitchenTicketResponse(
    Guid Id,
    int TicketNumber,
    string Status,
    IReadOnlyList<KitchenItemResponse> Items);