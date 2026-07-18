using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Tables;
using JLek.POS.Domain.ValueObjects;
using JLek.POS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JLek.POS.Infrastructure.Repositories;

public sealed class DiningTableRepository : IDiningTableRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DiningTableRepository(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DiningTable?> GetByIdAsync(
        TableId id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.DiningTables
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<DiningTable>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.DiningTables
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        DiningTable table,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.DiningTables.AddAsync(
            table,
            cancellationToken);

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }

    public async Task UpdateAsync(
        DiningTable table,
        CancellationToken cancellationToken = default)
    {
        _dbContext.DiningTables.Update(table);

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }
}