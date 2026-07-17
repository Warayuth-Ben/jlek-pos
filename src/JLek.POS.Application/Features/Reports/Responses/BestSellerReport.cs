namespace JLek.POS.Application.Features.Reports.Responses;

public sealed record BestSellerReport
{
    public DateOnly DateFrom { get; init; }
    public DateOnly DateTo { get; init; }
    public int Limit { get; init; }
    public IReadOnlyList<BestSellerItem> Items { get; init; } = [];
}

public sealed record BestSellerItem
{
    public int Rank { get; init; }
    public Guid MenuItemId { get; init; }
    public int TotalQuantity { get; init; }
    public decimal TotalRevenue { get; init; }
    public int OrderCount { get; init; }
}