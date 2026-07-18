using System.Net.Http.Json;
using JLek.POS.Web.Contracts.Orders;

namespace JLek.POS.Web.Clients.Orders;

public sealed class OrderClient : IOrderClient
{
    private readonly HttpClient _http;

    public OrderClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<OrderResponse> CreateAsync(Guid tableId, CancellationToken ct = default)
    {
        var response = await _http.PostAsync($"/tables/{tableId}/orders", null, ct);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<OrderResponse>(cancellationToken: ct))!;
    }

    public async Task<OrderResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<OrderResponse>($"/orders/{id}", ct);
    }

    public async Task<OrderResponse> AddItemAsync(Guid orderId, Guid menuItemId, int quantity, decimal unitPrice, CancellationToken ct = default)
    {
        var payload = new { menuItemId, quantity, unitPrice };
        var response = await _http.PostAsJsonAsync($"/orders/{orderId}/items", payload, ct);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<OrderResponse>(cancellationToken: ct))!;
    }

    public async Task<OrderResponse> ConfirmAsync(Guid orderId, CancellationToken ct = default)
    {
        var response = await _http.PostAsync($"/orders/{orderId}/confirm", null, ct);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<OrderResponse>(cancellationToken: ct))!;
    }
}