using JLek.POS.Domain.Payments;

namespace JLek.POS.Application.Features.Payments.Commands.RefundPayment;

public sealed record RefundPaymentCommand(
    PaymentId PaymentId,
    string? Reason);