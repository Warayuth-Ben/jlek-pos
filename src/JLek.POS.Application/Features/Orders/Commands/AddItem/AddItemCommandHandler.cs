using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Common.ValueObjects;

namespace JLek.POS.Application.Features.Orders.Commands.AddItem;

public sealed class AddItemCommandHandler
{
    private readonly IOrderRepository _repository;

    public AddItemCommandHandler(
        IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        AddItemCommand command,
        CancellationToken cancellationToken = default)
    {
        var order = await _repository.GetByIdAsync(
            command.OrderId,
            cancellationToken);

        if (order is null)
        {
            throw new InvalidOperationException("Order not found.");
        }

        order.AddItem(
            command.MenuItemId,
            Quantity.From(command.Quantity),
            Money.From(command.UnitPrice));

        await _repository.UpdateAsync(
            order,
            cancellationToken);
    }
}