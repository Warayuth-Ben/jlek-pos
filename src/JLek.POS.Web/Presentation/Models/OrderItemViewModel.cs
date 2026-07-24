namespace JLek.POS.Web.Presentation.Models;

public sealed record OrderItemViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; init; }
    public decimal TotalPrice { get; set; }
    public string Note { get; init; } = string.Empty;
}
