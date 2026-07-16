using JLek.POS.Domain.Catalog;

namespace JLek.POS.Application.Abstractions.Repositories;

public interface IIngredientRepository
{
    Task<Ingredient?> GetByIdAsync(
        IngredientId id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Ingredient>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Ingredient ingredient,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Ingredient ingredient,
        CancellationToken cancellationToken = default);
}