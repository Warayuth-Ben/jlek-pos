using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Catalog;
using JLek.POS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JLek.POS.Infrastructure.Repositories;

public sealed class IngredientRepository : IIngredientRepository
{
    private readonly ApplicationDbContext _dbContext;

    public IngredientRepository(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Ingredient?> GetByIdAsync(
        IngredientId id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Ingredients
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Ingredient>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Ingredients
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Ingredient ingredient,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Ingredients.AddAsync(
            ingredient,
            cancellationToken);

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }

    public async Task UpdateAsync(
        Ingredient ingredient,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Ingredients.Update(ingredient);

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }
}