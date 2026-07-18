using System.Net.Http.Json;
using JLek.POS.Web.Contracts.Tables;

namespace JLek.POS.Web.Clients.Tables;

public sealed class TableClient : ITableClient
{
    private readonly HttpClient _http;

    public TableClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<TableResponse>> GetAllAsync(CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<List<TableResponse>>("/tables", ct)
               ?? [];
    }

    public async Task<List<TableResponse>> GetAvailableAsync(CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<List<TableResponse>>("/tables/available", ct)
               ?? [];
    }

    public async Task<TableResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<TableResponse>($"/tables/{id}", ct);
    }

    public async Task<TableResponse> OpenAsync(Guid id, CancellationToken ct = default)
    {
        var response = await _http.PostAsync($"/tables/{id}/open", null, ct);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<TableResponse>(cancellationToken: ct))!;
    }

    public async Task<TableResponse> ReleaseAsync(Guid id, CancellationToken ct = default)
    {
        var response = await _http.PostAsync($"/tables/{id}/release", null, ct);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<TableResponse>(cancellationToken: ct))!;
    }
}
