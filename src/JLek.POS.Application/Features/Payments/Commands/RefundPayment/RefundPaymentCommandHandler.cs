using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Payments.Responses;

namespace JLek.POS.Application.Features.Payments.Commands.RefundPayment;

public sealed class RefundPaymentCommandHandler
{
    private readonly IPaymentRepository _repository;

    public RefundPaymentCommandHandler(
        IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaymentResponse> Handle(
        RefundPaymentCommand command,
        CancellationToken cancellationToken = default)
    {
        var payment = await _repository.GetByIdAsync(
            command.PaymentId,
            cancellationToken);

        if (payment is null)
        {
            throw new InvalidOperationException("Payment not found.");
        }

        payment.Refund(command.Reason);

        await _repository.UpdateAsync(payment, cancellationToken);

        return PaymentResponse.FromDomain(payment);
    }
}