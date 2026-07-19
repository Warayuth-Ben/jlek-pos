using JLek.POS.Web.Contracts.Menu;

namespace JLek.POS.Web.Clients.Menu;

public interface IMenuClient
{
    Task<List<ProductCategoryResponse>> GetCategoriesAsync(CancellationToken ct = default);
    Task<List<ProductResponse>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken ct = default);
    Task<List<ProductResponse>> GetProductsAsync(CancellationToken ct = default);
}