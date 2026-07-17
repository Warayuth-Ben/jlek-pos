namespace JLek.POS.Application.Features.Reports.Queries.SalesByPayment;

public sealed record GetSalesByPaymentReportQuery(
    DateOnly? DateFrom = null,
    DateOnly? DateTo = null);