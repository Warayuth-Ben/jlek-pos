namespace JLek.POS.Application.Features.Receipt.DTOs;

public sealed record CustomerReceiptData
{
    public string ReceiptNumber { get; init; } = string.Empty;
    public DateTime PrintedAt { get; init; }
    public string? TableName { get; init; }
    public IReadOnlyList<ReceiptItemData> Items { get; init; } = Array.Empty<ReceiptItemData>();
    public decimal Total { get; init; }
    public decimal AmountReceived { get; init; }
    public decimal Change { get; init; }
    public string PaymentMethod { get; init; } = string.Empty;
}