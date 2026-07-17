namespace JLek.POS.Application.Features.Receipt.Configuration;

public sealed record ReceiptConfiguration
{
    public string ShopName { get; init; } = string.Empty;
    public string ShopAddress { get; init; } = string.Empty;
    public string ShopPhone { get; init; } = string.Empty;
    public string TaxId { get; init; } = string.Empty;
    public string? PromptPayQR { get; init; }
    public string Footer { get; init; } = "Thank you";
    public int PaperWidth { get; init; } = 48;
    public string DefaultPrinter { get; init; } = "null";
}