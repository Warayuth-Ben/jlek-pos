namespace JLek.POS.Web.Presentation.Models;

public sealed record MenuItemViewModel
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string Category { get; init; } = string.Empty;
    public bool Available { get; init; }
    public bool IsSelected { get; set; }
}
