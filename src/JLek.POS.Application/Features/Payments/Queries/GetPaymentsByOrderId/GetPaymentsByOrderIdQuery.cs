using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Application.Features.Payments.Queries.GetPaymentsByOrderId;

public sealed record GetPaymentsByOrderIdQuery(
    OrderId OrderId);