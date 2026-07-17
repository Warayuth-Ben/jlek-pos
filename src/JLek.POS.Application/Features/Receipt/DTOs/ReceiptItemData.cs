namespace JLek.POS.Application.Features.Receipt.DTOs;

public sealed record ReceiptItemData
{
    public string Name { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal Total { get; init; }
}