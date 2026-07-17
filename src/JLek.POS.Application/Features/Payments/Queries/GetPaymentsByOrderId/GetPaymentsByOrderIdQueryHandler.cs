using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Payments.Responses;

namespace JLek.POS.Application.Features.Payments.Queries.GetPaymentsByOrderId;

public sealed class GetPaymentsByOrderIdQueryHandler
{
    private readonly IPaymentRepository _repository;

    public GetPaymentsByOrderIdQueryHandler(
        IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<PaymentResponse>> Handle(
        GetPaymentsByOrderIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var payments = await _repository.GetAllAsync(cancellationToken);

        return payments
            .Where(p => p.OrderId == query.OrderId)
            .Select(PaymentResponse.FromDomain)
            .ToList();
    }
}