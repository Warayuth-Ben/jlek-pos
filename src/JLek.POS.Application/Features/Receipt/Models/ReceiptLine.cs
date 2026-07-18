namespace JLek.POS.Application.Features.Receipt.Models;

public sealed record ReceiptLine
{
    public string Text { get; init; } = string.Empty;
    public ReceiptAlignment Alignment { get; init; } = ReceiptAlignment.Left;
    public bool Bold { get; init; }
    public bool DoubleWidth { get; init; }
    public bool DoubleHeight { get; init; }
}

public enum ReceiptAlignment
{
    Left,
    Center,
    Right
}