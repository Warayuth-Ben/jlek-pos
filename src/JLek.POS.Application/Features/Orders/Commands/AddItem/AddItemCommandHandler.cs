using JLek.POS.Application.Abstractions;

namespace JLek.POS.Application.Features.Orders.Commands.AddItem;

public sealed class AddItemCommandHandler
    : ICommandHandler<AddItemCommand>
{
    public Task Handle(
        AddItemCommand command,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}