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
        return await _http.GetFromJsonAsync<List<ProductCategoryResponse>>("/categories", ct) ?? [];
    }

    public async Task<List<ProductResponse>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken ct = default)
    {
        return await _http.GetFromJsonAsync<List<ProductResponse>>($"/products?categoryId={categoryId}", ct) ?? [];
    }
}