using JLek.POS.Domain.Orders;
using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Application.Abstractions.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(
        OrderId id,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Order order,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Order order,
        CancellationToken cancellationToken = default);
}