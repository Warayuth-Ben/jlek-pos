namespace JLek.POS.Web.Presentation.Dtos;

public sealed record MenuItemDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string Category { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
}