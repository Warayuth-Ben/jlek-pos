namespace JLek.POS.Application.Features.Reports.Queries.BestSellers;

public sealed record GetBestSellerReportQuery(
    DateOnly? DateFrom = null,
    DateOnly? DateTo = null,
    int Limit = 10);