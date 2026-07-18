namespace JLek.POS.Application.Features.Receipt.DTOs;

public sealed record RefundReceiptData
{
    public string ReceiptNumber { get; init; } = string.Empty;
    public string OriginalReceiptNumber { get; init; } = string.Empty;
    public DateTime PrintedAt { get; init; }
    public decimal AmountRefunded { get; init; }
    public string? Reason { get; init; }
    public string PaymentMethod { get; init; } = string.Empty;
}