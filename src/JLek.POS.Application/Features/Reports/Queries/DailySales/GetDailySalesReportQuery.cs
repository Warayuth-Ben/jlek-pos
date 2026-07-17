namespace JLek.POS.Application.Features.Reports.Queries.DailySales;

public sealed record GetDailySalesReportQuery(
    DateOnly? Date = null);