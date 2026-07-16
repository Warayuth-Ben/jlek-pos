using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Abstractions.Repositories;

public interface IProductCategoryRepository
{
    Task<ProductCategory?> GetByIdAsync(
        ProductCategoryId id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProductCategory>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        ProductCategory productCategory,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        ProductCategory productCategory,
        CancellationToken cancellationToken = default);
}