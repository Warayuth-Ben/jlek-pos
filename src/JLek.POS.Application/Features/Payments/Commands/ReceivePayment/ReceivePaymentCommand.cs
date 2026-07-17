using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.Payments;

namespace JLek.POS.Application.Features.Payments.Commands.ReceivePayment;

public sealed record ReceivePaymentCommand(
    OrderId OrderId,
    decimal AmountReceived,
    PaymentMethod Method);