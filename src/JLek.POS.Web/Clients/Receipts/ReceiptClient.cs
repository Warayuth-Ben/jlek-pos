using System.Net.Http.Json;
using JLek.POS.Web.Contracts.Payments;

namespace JLek.POS.Web.Clients.Receipts;

public sealed class ReceiptClient : IReceiptClient
{
    private readonly HttpClient _http;

    public ReceiptClient(HttpClient http)
    {
        _http = http;
    }

    public async Task PrintCustomerReceiptAsync(Guid orderId, CancellationToken ct = default)
    {
        var payload = new { OrderId = orderId.ToString() };
        var response = await _http.PostAsJsonAsync("/receipts/customer-print", payload, ct);
        response.EnsureSuccessStatusCode();
    }
}