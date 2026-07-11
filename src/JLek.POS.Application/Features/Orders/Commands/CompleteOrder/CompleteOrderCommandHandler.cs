using JLek.POS.Application.Abstractions.Repositories;

namespace JLek.POS.Application.Features.Orders.Commands.CompleteOrder;

public sealed class CompleteOrderCommandHandler
{
    private readonly IOrderRepository _repository;

    public CompleteOrderCommandHandler(
        IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        CompleteOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var order = await _repository.GetByIdAsync(
            command.OrderId,
            cancellationToken);

        if (order is null)
        {
            throw new InvalidOperationException("Order not found.");
        }

        order.Complete();

        await _repository.UpdateAsync(
            order,
            cancellationToken);
    }
}