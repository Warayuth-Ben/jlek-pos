using JLek.POS.Application.Abstractions;
using JLek.POS.Application.Features.Reports.Responses;
using JLek.POS.Domain.Common.Abstractions;
using JLek.POS.Domain.Orders;
using JLek.POS.Domain.Payments;
using Microsoft.EntityFrameworkCore;

namespace JLek.POS.Application.Features.Reports.Queries.DailySales;

public sealed class GetDailySalesReportQueryHandler
{
    private readonly IReportingDbContext _context;
    private readonly IClock _clock;

    public GetDailySalesReportQueryHandler(
        IReportingDbContext context,
        IClock clock)
    {
        _context = context;
        _clock = clock;
    }

    public async Task<DailySalesReport> Handle(
        GetDailySalesReportQuery query,
        CancellationToken cancellationToken = default)
    {
        var today = query.Date ?? DateOnly.FromDateTime(_clock.Today);

        var completedOrderIds = await _context.Orders
            .AsNoTracking()
            .Where(o => o.Status == OrderStatus.Completed)
            .Select(o => o.Id)
            .ToListAsync(cancellationToken);

        var completedPayments = await _context.Payments
            .AsNoTracking()
            .Where(p => completedOrderIds.Contains(p.OrderId)
                     && p.Status == PaymentStatus.Completed)
            .ToListAsync(cancellationToken);

        var totalRevenue = completedPayments.Sum(p => p.AmountReceived.Amount);
        var totalOrders = completedPayments.Count;
        var averageOrderValue = totalOrders > 0
            ? totalRevenue / totalOrders
            : 0m;

        var orderIds = completedPayments.Select(p => p.OrderId).ToList();
        var totalItemsSold = await _context.Orders
            .AsNoTracking()
            .Where(o => orderIds.Contains(o.Id))
            .SelectMany(o => o.Items)
            .SumAsync(i => i.Quantity.Value, cancellationToken);

        var totalRefunds = await _context.Payments
            .AsNoTracking()
            .Where(p => orderIds.Contains(p.OrderId)
                     && p.Status == PaymentStatus.Refunded)
            .SumAsync(p => p.AmountReceived.Amount, cancellationToken);

        return new DailySalesReport
        {
            Date = today,
            TotalOrders = totalOrders,
            TotalRevenue = totalRevenue,
            TotalRefunds = totalRefunds,
            NetRevenue = totalRevenue - totalRefunds,
            AverageOrderValue = averageOrderValue,
            TotalItemsSold = totalItemsSold
        };
    }
}