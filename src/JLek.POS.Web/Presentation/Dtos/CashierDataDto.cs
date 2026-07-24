namespace JLek.POS.Web.Presentation.Dtos;

public sealed record CashierDataDto
{
    public IReadOnlyList<TableDto> Tables { get; init; } = Array.Empty<TableDto>();
    public IReadOnlyList<MenuItemDto> MenuItems { get; init; } = Array.Empty<MenuItemDto>();
}