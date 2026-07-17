using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Common.ValueObjects;
using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Domain.Payments.Events;

public sealed class PaymentRefundedEvent : DomainEvent
{
    public PaymentRefundedEvent(
        PaymentId paymentId,
        OrderId orderId,
        Money amountReceived,
        string? reason)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        AmountReceived = amountReceived;
        Reason = reason;
    }

    public PaymentId PaymentId { get; }
    public OrderId OrderId { get; }
    public Money AmountReceived { get; }
    public string? Reason { get; }
}