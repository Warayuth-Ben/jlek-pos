using JLek.POS.Domain.Kitchen;

namespace JLek.POS.Application.Abstractions.Repositories;

public interface IKitchenTicketRepository
{
    Task<KitchenTicket?> GetByIdAsync(
        KitchenTicketId id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<KitchenTicket>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        KitchenTicket ticket,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        KitchenTicket ticket,
        CancellationToken cancellationToken = default);
}