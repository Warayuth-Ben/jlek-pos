using JLek.POS.Application.Abstractions.Repositories;

namespace JLek.POS.Application.Features.Orders.Commands.ConfirmOrder;

public sealed class ConfirmOrderCommandHandler
{
    private readonly IOrderRepository _repository;

    public ConfirmOrderCommandHandler(
        IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        ConfirmOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var order = await _repository.GetByIdAsync(
            command.OrderId,
            cancellationToken);

        if (order is null)
        {
            throw new InvalidOperationException("Order not found.");
        }

        order.Confirm();

        await _repository.UpdateAsync(
            order,
            cancellationToken);
    }
}