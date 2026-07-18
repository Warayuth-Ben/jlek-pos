namespace JLek.POS.Application.Features.Receipt.DTOs;

public sealed record KitchenTicketReceiptData
{
    public int TicketNumber { get; init; }
    public DateTime PrintedAt { get; init; }
    public IReadOnlyList<KitchenReceiptItemData> Items { get; init; } = Array.Empty<KitchenReceiptItemData>();
}

public sealed record KitchenReceiptItemData
{
    public string Name { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public string? Notes { get; init; }
}