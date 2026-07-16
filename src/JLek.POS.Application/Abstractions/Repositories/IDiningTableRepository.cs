using JLek.POS.Domain.Tables;
using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Application.Abstractions.Repositories;

public interface IDiningTableRepository
{
    Task<DiningTable?> GetByIdAsync(
        TableId id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DiningTable>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        DiningTable table,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        DiningTable table,
        CancellationToken cancellationToken = default);
}