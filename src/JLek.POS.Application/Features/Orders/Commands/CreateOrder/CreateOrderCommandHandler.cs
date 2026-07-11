using JLek.POS.Application.Abstractions;

namespace JLek.POS.Application.Features.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler
    : ICommandHandler<CreateOrderCommand>
{
    public Task Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}