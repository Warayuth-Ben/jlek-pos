using JLek.POS.Web.Contracts.Reports;

namespace JLek.POS.Web.Clients.Reports;

public interface IReportClient
{
    Task<DailySalesReport> GetDailySalesAsync(DateOnly? date = null, CancellationToken ct = default);
    Task<SalesByPaymentReport> GetSalesByPaymentAsync(DateOnly? from = null, DateOnly? to = null, CancellationToken ct = default);
    Task<BestSellerReport> GetBestSellersAsync(DateOnly? from = null, DateOnly? to = null, int? limit = null, CancellationToken ct = default);
}