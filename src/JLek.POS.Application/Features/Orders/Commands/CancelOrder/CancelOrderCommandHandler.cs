using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Orders;

namespace JLek.POS.Application.Features.Orders.Commands.CancelOrder;

public sealed class CancelOrderCommandHandler
{
    private readonly IOrderRepository _repository;

    public CancelOrderCommandHandler(
        IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Order> Handle(
        CancelOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var order = await _repository.GetByIdAsync(
            command.OrderId,
            cancellationToken);

        if (order is null)
        {
            throw new InvalidOperationException("Order not found.");
        }

        order.Cancel();

        await _repository.UpdateAsync(
            order,
            cancellationToken);

        return order;
    }
}