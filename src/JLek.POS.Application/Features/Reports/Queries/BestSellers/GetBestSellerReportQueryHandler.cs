using JLek.POS.Application.Abstractions;
using JLek.POS.Application.Features.Reports.Responses;
using JLek.POS.Domain.Common.Abstractions;
using JLek.POS.Domain.Orders;
using Microsoft.EntityFrameworkCore;

namespace JLek.POS.Application.Features.Reports.Queries.BestSellers;

public sealed class GetBestSellerReportQueryHandler
{
    private readonly IReportingDbContext _context;
    private readonly IClock _clock;

    public GetBestSellerReportQueryHandler(
        IReportingDbContext context,
        IClock clock)
    {
        _context = context;
        _clock = clock;
    }

    public async Task<BestSellerReport> Handle(
        GetBestSellerReportQuery query,
        CancellationToken cancellationToken = default)
    {
        var dateTo = query.DateTo ?? DateOnly.FromDateTime(_clock.Today);
        var dateFrom = query.DateFrom ?? dateTo.AddDays(-30);

        var bestSellerData = await _context.Orders
            .AsNoTracking()
            .Where(o => o.Status == OrderStatus.Completed)
            .SelectMany(o => o.Items)
            .GroupBy(i => i.MenuItemId)
            .Select(g => new
            {
                MenuItemId = g.Key,
                TotalQuantity = g.Sum(i => i.Quantity.Value),
                TotalRevenue = g.Sum(i => i.UnitPrice.Amount * i.Quantity.Value),
                OrderCount = g.Count()
            })
            .OrderByDescending(x => x.TotalQuantity)
            .Take(query.Limit)
            .ToListAsync(cancellationToken);

        var items = bestSellerData
            .Select((item, index) => new BestSellerItem
            {
                Rank = index + 1,
                MenuItemId = item.MenuItemId,
                TotalQuantity = item.TotalQuantity,
                TotalRevenue = item.TotalRevenue,
                OrderCount = item.OrderCount
            })
            .ToList();

        return new BestSellerReport
        {
            DateFrom = dateFrom,
            DateTo = dateTo,
            Limit = query.Limit,
            Items = items
        };
    }
}