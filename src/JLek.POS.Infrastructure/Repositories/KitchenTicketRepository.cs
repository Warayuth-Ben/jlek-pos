using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Kitchen;
using JLek.POS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JLek.POS.Infrastructure.Repositories;

public sealed class KitchenTicketRepository : IKitchenTicketRepository
{
    private readonly ApplicationDbContext _dbContext;

    public KitchenTicketRepository(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<KitchenTicket?> GetByIdAsync(
        KitchenTicketId id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.KitchenTickets
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<KitchenTicket>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.KitchenTickets
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        KitchenTicket ticket,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.KitchenTickets.AddAsync(
            ticket,
            cancellationToken);

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }

    public async Task UpdateAsync(
        KitchenTicket ticket,
        CancellationToken cancellationToken = default)
    {
        _dbContext.KitchenTickets.Update(ticket);

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }
}