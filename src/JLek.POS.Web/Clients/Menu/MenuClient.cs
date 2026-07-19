using System.Net.Http.Json;
using System.Text.Json;
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
        var doc = await _http.GetFromJsonAsync<JsonDocument>("/categories", ct);
        if (doc is null) return [];
        return doc.RootElement.EnumerateArray().Select(e =>
        {
            var sortOrder = e.TryGetProperty("displayOrder", out var doEl) && doEl.ValueKind == JsonValueKind.Number
                ? doEl.GetInt32()
                : 0;
            return new ProductCategoryResponse(
                e.GetProperty("id").GetGuid(),
                e.GetProperty("name").GetString() ?? "",
                sortOrder
            );
        }).ToList();
    }

    public async Task<List<ProductResponse>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken ct = default)
    {
        var doc = await _http.GetFromJsonAsync<JsonDocument>($"/products?categoryId={categoryId}", ct);
        if (doc is null) return [];
        return ParseProductResponses(doc);
    }

    public async Task<List<ProductResponse>> GetProductsAsync(CancellationToken ct = default)
    {
        var doc = await _http.GetFromJsonAsync<JsonDocument>("/products", ct);
        if (doc is null) return [];
        return ParseProductResponses(doc);
    }

    private static List<ProductResponse> ParseProductResponses(JsonDocument doc)
    {
        return doc.RootElement.EnumerateArray().Select(e =>
        {
            var status = e.GetProperty("status").GetString() ?? "";
            var isAvailable = status is "Available" or "available";
            var price = 0m;
            if (e.TryGetProperty("suggestedPrices", out var pricesEl) && pricesEl.ValueKind == JsonValueKind.Array)
            {
                var prices = pricesEl.EnumerateArray().ToList();
                if (prices.Count > 0)
                    price = prices[0].GetDecimal();
            }
            return new ProductResponse(
                e.GetProperty("id").GetGuid(),
                e.GetProperty("name").GetString() ?? "",
                price,
                isAvailable
            );
        }).ToList();
    }
}