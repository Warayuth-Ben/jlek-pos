using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Catalog;
using JLek.POS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JLek.POS.Infrastructure.Repositories;

public sealed class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductCategoryRepository(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductCategory?> GetByIdAsync(
        ProductCategoryId id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.ProductCategories
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<ProductCategory>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.ProductCategories
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        ProductCategory productCategory,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.ProductCategories.AddAsync(
            productCategory,
            cancellationToken);

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }

    public async Task UpdateAsync(
        ProductCategory productCategory,
        CancellationToken cancellationToken = default)
    {
        _dbContext.ProductCategories.Update(productCategory);

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }
}