using System.Net.Http.Json;
using JLek.POS.Web.Contracts.Menu;

namespace JLek.POS.Web.Clients.Menu;

public sealed class MenuClient : IMenuClient
{
    private readonly HttpClient _http;

    public MenuClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ProductCategoryResponse>> GetCategoriesAsync(CancellationToken ct = default)
    {
        // API returns strongly-typed IDs, deserialize as dynamic
        var doc = await _http.GetFromJsonAsync<System.Text.Json.JsonDocument>("/categories", ct);
        if (doc is null) return [];
        return doc.RootElement.EnumerateArray().Select(e => new ProductCategoryResponse(
            Guid.Parse(e.GetProperty("id").GetProperty("value").GetString()!),
            e.GetProperty("name").GetString() ?? "",
            0
        )).ToList();
    }

    public async Task<List<ProductResponse>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken ct = default)
    {
        var doc = await _http.GetFromJsonAsync<System.Text.Json.JsonDocument>($"/products?categoryId={categoryId}", ct);
        if (doc is null) return [];
        return doc.RootElement.EnumerateArray().Select(e => new ProductResponse(
            Guid.Parse(e.GetProperty("id").GetProperty("value").GetString()!),
            e.GetProperty("name").GetString() ?? "",
            0m,
            e.GetProperty("status").GetInt32() == 0
        )).ToList();
    }

    public async Task<List<ProductResponse>> GetProductsAsync(CancellationToken ct = default)
    {
        var doc = await _http.GetFromJsonAsync<System.Text.Json.JsonDocument>("/products", ct);
        if (doc is null) return [];
        return doc.RootElement.EnumerateArray().Select(e => new ProductResponse(
            Guid.Parse(e.GetProperty("id").GetProperty("value").GetString()!),
            e.GetProperty("name").GetString() ?? "",
            0m,
            e.GetProperty("status").GetInt32() == 0
        )).ToList();
    }
}