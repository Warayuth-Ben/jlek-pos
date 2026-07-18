using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Common.ValueObjects;
using JLek.POS.Domain.Orders.ValueObjects;

namespace JLek.POS.Domain.Payments.Events;

public sealed class PaymentReceivedEvent : DomainEvent
{
    public PaymentReceivedEvent(
        PaymentId paymentId,
        OrderId orderId,
        Money amountReceived,
        Money change)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        AmountReceived = amountReceived;
        Change = change;
    }

    public PaymentId PaymentId { get; }
    public OrderId OrderId { get; }
    public Money AmountReceived { get; }
    public Money Change { get; }
}