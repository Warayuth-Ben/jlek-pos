namespace JLek.POS.Web.Presentation.Models;

public sealed record OrderSummaryViewModel
{
    public decimal Subtotal { get; init; }
    public decimal Discount { get; init; }
    public decimal Total { get; init; }
    public int ItemCount { get; init; }
}