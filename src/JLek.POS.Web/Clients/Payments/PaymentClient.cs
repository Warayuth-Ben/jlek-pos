using System.Net.Http.Json;
using JLek.POS.Web.Contracts.Payments;

namespace JLek.POS.Web.Clients.Payments;

public sealed class PaymentClient : IPaymentClient
{
    private readonly HttpClient _http;

    public PaymentClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<PaymentResponse> ReceivePaymentAsync(Guid orderId, decimal amount, string method, CancellationToken ct = default)
    {
        var payload = new { OrderId = orderId.ToString(), Amount = amount, Method = method };
        var response = await _http.PostAsJsonAsync("/payments", payload, ct);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<PaymentResponse>(cancellationToken: ct))!;
    }
}