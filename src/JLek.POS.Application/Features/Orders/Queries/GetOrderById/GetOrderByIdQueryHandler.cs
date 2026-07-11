using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Orders;

namespace JLek.POS.Application.Features.Orders.Queries.GetOrderById;

public sealed class GetOrderByIdQueryHandler
{
    private readonly IOrderRepository _repository;

    public GetOrderByIdQueryHandler(
        IOrderRepository repository)
    {
        _repository = repository;
    }

    public Task<Order?> Handle(
        GetOrderByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetByIdAsync(
            query.OrderId,
            cancellationToken);
    }
}