using JLek.POS.Domain.Common.ValueObjects;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.Payments;

namespace JLek.POS.Application.Features.Payments.Responses;

public record PaymentResponse(
    Guid Id,
    Guid OrderId,
    decimal OrderTotal,
    decimal AmountReceived,
    decimal Change,
    string Method,
    string Status,
    string? RefundReason)
{
    public static PaymentResponse FromDomain(Payment payment)
    {
        return new PaymentResponse(
            payment.Id.Value,
            payment.OrderId.Value,
            payment.OrderTotal.Amount,
            payment.AmountReceived.Amount,
            payment.Change.Amount,
            payment.Method.ToString(),
            payment.Status.ToString(),
            payment.RefundReason);
    }
}