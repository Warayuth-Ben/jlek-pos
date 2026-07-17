using JLek.POS.Application.Abstractions;
using JLek.POS.Application.Features.Reports.Responses;
using JLek.POS.Domain.Common.Abstractions;
using JLek.POS.Domain.Payments;
using Microsoft.EntityFrameworkCore;

namespace JLek.POS.Application.Features.Reports.Queries.SalesByPayment;

public sealed class GetSalesByPaymentReportQueryHandler
{
    private readonly IReportingDbContext _context;
    private readonly IClock _clock;

    public GetSalesByPaymentReportQueryHandler(
        IReportingDbContext context,
        IClock clock)
    {
        _context = context;
        _clock = clock;
    }

    public async Task<SalesByPaymentReport> Handle(
        GetSalesByPaymentReportQuery query,
        CancellationToken cancellationToken = default)
    {
        var dateTo = query.DateTo ?? DateOnly.FromDateTime(_clock.Today);
        var dateFrom = query.DateFrom ?? dateTo.AddDays(-6);

        var payments = await _context.Payments
            .AsNoTracking()
            .Where(p => p.Status == PaymentStatus.Completed)
            .ToListAsync(cancellationToken);

        var methodGroups = payments
            .GroupBy(p => p.Method)
            .Select(g => new PaymentMethodSummary
            {
                Method = g.Key.ToString(),
                TransactionCount = g.Count(),
                TotalAmount = g.Sum(p => p.AmountReceived.Amount)
            })
            .OrderByDescending(x => x.TotalAmount)
            .ToList();

        return new SalesByPaymentReport
        {
            DateFrom = dateFrom,
            DateTo = dateTo,
            PaymentMethods = methodGroups
        };
    }
}