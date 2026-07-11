using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Orders;
using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Infrastructure.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    public Task<Order?> GetByIdAsync(
        OrderId id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(
        Order order,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(
        Order order,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}