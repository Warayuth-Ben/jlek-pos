namespace JLek.POS.Web.Presentation.Models;

public sealed record CashierWorkspaceViewModel
{
    public IReadOnlyList<TableViewModel> Tables { get; init; } = Array.Empty<TableViewModel>();
    public IReadOnlyList<MenuItemViewModel> Menu { get; init; } = Array.Empty<MenuItemViewModel>();
    public IReadOnlyList<OrderItemViewModel> OrderItems { get; init; } = Array.Empty<OrderItemViewModel>();
    public OrderSummaryViewModel Summary { get; init; } = new();
}