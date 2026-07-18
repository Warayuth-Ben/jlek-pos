namespace JLek.POS.Application.Features.Receipt.Models;

public sealed record ReceiptDocument
{
    public string Title { get; init; } = string.Empty;
    public string? ReprintLabel { get; init; }
    public string? ReceiptNumber { get; init; }
    public IReadOnlyList<ReceiptLine> Lines { get; init; } = Array.Empty<ReceiptLine>();
}