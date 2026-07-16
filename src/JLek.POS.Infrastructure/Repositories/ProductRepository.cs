using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Catalog;
using JLek.POS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JLek.POS.Infrastructure.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetByIdAsync(
        ProductId id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products
            .Include(x => x.OptionGroups)
                .ThenInclude(g => g.Options)
            .Include(x => x.Modifiers)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products
            .Include(x => x.OptionGroups)
                .ThenInclude(g => g.Options)
            .Include(x => x.Modifiers)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Product product,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Products.AddAsync(
            product,
            cancellationToken);

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }

    public async Task UpdateAsync(
        Product product,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Products.Update(product);

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }
}