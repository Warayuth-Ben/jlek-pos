namespace JLek.POS.Application.Features.Reports.Responses;

public sealed record DailySalesReport
{
    public DateOnly Date { get; init; }
    public int TotalOrders { get; init; }
    public decimal TotalRevenue { get; init; }
    public decimal TotalRefunds { get; init; }
    public decimal NetRevenue { get; init; }
    public decimal AverageOrderValue { get; init; }
    public int TotalItemsSold { get; init; }
}