using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Payments.Responses;

namespace JLek.POS.Application.Features.Payments.Queries.GetPaymentById;

public sealed class GetPaymentByIdQueryHandler
{
    private readonly IPaymentRepository _repository;

    public GetPaymentByIdQueryHandler(
        IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaymentResponse?> Handle(
        GetPaymentByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var payment = await _repository.GetByIdAsync(
            query.PaymentId,
            cancellationToken);

        return payment is not null
            ? PaymentResponse.FromDomain(payment)
            : null;
    }
}