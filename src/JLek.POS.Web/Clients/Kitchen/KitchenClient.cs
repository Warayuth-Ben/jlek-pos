using System.Net.Http.Json;
using JLek.POS.Web.Contracts.Kitchen;

namespace JLek.POS.Web.Clients.Kitchen;

public sealed class KitchenClient : IKitchenClient
{
    private readonly HttpClient _http;

    public KitchenClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<KitchenTicketResponse>> GetActiveAsync(CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<List<KitchenTicketResponse>>("/kitchen/active", ct) ?? [];
    }

    public async Task<List<KitchenTicketResponse>> GetAllAsync(CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<List<KitchenTicketResponse>>("/kitchen", ct) ?? [];
    }

    public async Task StartPreparationAsync(Guid id, CancellationToken ct = default)
    {
        var response = await _http.PostAsync($"/kitchen/{id}/start", null, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task CompletePreparationAsync(Guid id, CancellationToken ct = default)
    {
        var response = await _http.PostAsync($"/kitchen/{id}/complete", null, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task ServeAsync(Guid id, CancellationToken ct = default)
    {
        var response = await _http.PostAsync($"/kitchen/{id}/serve", null, ct);
        response.EnsureSuccessStatusCode();
    }
}