using System.Net.Http.Json;
using JLek.POS.Web.Contracts.Reports;

namespace JLek.POS.Web.Clients.Reports;

public sealed class ReportClient : IReportClient
{
    private readonly HttpClient _http;

    public ReportClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<DailySalesReport> GetDailySalesAsync(DateOnly? date = null, CancellationToken ct = default)
    {
        var query = date.HasValue ? $"?date={date.Value:yyyy-MM-dd}" : "";
        return await _http.GetFromJsonAsync<DailySalesReport>($"/reports/daily-sales{query}", ct)
               ?? new DailySalesReport("", 0, 0, 0, 0, 0, 0);
    }

    public async Task<SalesByPaymentReport> GetSalesByPaymentAsync(DateOnly? from = null, DateOnly? to = null, CancellationToken ct = default)
    {
        var q = new List<string>();
        if (from.HasValue) q.Add($"dateFrom={from.Value:yyyy-MM-dd}");
        if (to.HasValue) q.Add($"dateTo={to.Value:yyyy-MM-dd}");
        var query = q.Count > 0 ? "?" + string.Join("&", q) : "";
        return await _http.GetFromJsonAsync<SalesByPaymentReport>($"/reports/sales-by-payment{query}", ct)
               ?? new SalesByPaymentReport("", "", []);
    }

    public async Task<BestSellerReport> GetBestSellersAsync(DateOnly? from = null, DateOnly? to = null, int? limit = null, CancellationToken ct = default)
    {
        var q = new List<string>();
        if (from.HasValue) q.Add($"dateFrom={from.Value:yyyy-MM-dd}");
        if (to.HasValue) q.Add($"dateTo={to.Value:yyyy-MM-dd}");
        if (limit.HasValue) q.Add($"limit={limit.Value}");
        var query = q.Count > 0 ? "?" + string.Join("&", q) : "";
        return await _http.GetFromJsonAsync<BestSellerReport>($"/reports/best-sellers{query}", ct)
               ?? new BestSellerReport("", "", 10, []);
    }
}