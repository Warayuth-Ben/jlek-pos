using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Abstractions.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(
        ProductId id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Product>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Product product,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Product product,
        CancellationToken cancellationToken = default);
}