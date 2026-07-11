using JLek.POS.Application.Abstractions;

namespace JLek.POS.Application.Features.Orders.Commands.ConfirmOrder;

public sealed class ConfirmOrderCommandHandler
    : ICommandHandler<ConfirmOrderCommand>
{
    public Task Handle(
        ConfirmOrderCommand command,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}