using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Orders;

namespace JLek.POS.Application.Features.Orders.Commands.RemoveItem;

public sealed class RemoveItemCommandHandler
{
    private readonly IOrderRepository _repository;

    public RemoveItemCommandHandler(
        IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Order> Handle(
        RemoveItemCommand command,
        CancellationToken cancellationToken = default)
    {
        var order = await _repository.GetByIdAsync(
            command.OrderId,
            cancellationToken);

        if (order is null)
        {
            throw new InvalidOperationException("Order not found.");
        }

        order.RemoveItem(command.OrderItemId);

        await _repository.UpdateAsync(
            order,
            cancellationToken);

        return order;
    }
}
