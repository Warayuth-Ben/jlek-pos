using JLek.POS.Domain.Payments;

namespace JLek.POS.Application.Features.Payments.Queries.GetPaymentById;

public sealed record GetPaymentByIdQuery(
    PaymentId PaymentId);