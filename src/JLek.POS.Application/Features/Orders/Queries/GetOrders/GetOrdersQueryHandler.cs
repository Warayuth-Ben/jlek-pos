using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Domain.Orders;

namespace JLek.POS.Application.Features.Orders.Queries.GetOrders;

public sealed class GetOrdersQueryHandler
{
    private readonly IOrderRepository _repository;

    public GetOrdersQueryHandler(
        IOrderRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Order>> Handle(
        GetOrdersQuery query,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(
            cancellationToken);
    }
}
