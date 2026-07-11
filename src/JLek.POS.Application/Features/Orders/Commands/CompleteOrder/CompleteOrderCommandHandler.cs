using JLek.POS.Application.Abstractions;

namespace JLek.POS.Application.Features.Orders.Commands.CompleteOrder;

public sealed class CompleteOrderCommandHandler
    : ICommandHandler<CompleteOrderCommand>
{
    public Task Handle(
        CompleteOrderCommand command,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}