using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Orders;

namespace JLek.POS.Application.Features.Orders.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler
{
    private readonly IOrderRepository _repository;

    public CreateOrderCommandHandler(
        IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Order> Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken = default)
    {
        var order = Order.Create();

        await _repository.AddAsync(
            order,
            cancellationToken);

        return order;
    }
}