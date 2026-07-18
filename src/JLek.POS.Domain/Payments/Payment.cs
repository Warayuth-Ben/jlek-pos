using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Common.ValueObjects;
using JLek.POS.Domain.Orders;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.Payments.Events;
using JLek.POS.Domain.Payments.Rules;

namespace JLek.POS.Domain.Payments;

public sealed class Payment : AggregateRoot<PaymentId>
{
    private string? _refundReason;

    // Constructor สำหรับ EF Core
    private Payment()
        : base(PaymentId.From(Guid.Empty))
    {
    }

    private Payment(
        PaymentId id,
        OrderId orderId,
        Money orderTotal,
        Money amountReceived,
        Money change,
        PaymentMethod method)
        : base(id)
    {
        OrderId = orderId;
        OrderTotal = orderTotal;
        AmountReceived = amountReceived;
        Change = change;
        Method = method;
        Status = PaymentStatus.Completed;
    }

    public OrderId OrderId { get; private set; }
    public Money OrderTotal { get; private set; }
    public Money AmountReceived { get; private set; }
    public Money Change { get; private set; }
    public PaymentMethod Method { get; private set; }
    public PaymentStatus Status { get; private set; }
    public string? RefundReason => _refundReason;

    public static Payment Create(
        Order order,
        Money amountReceived,
        PaymentMethod method)
    {
        CheckRule(new CannotPayCancelledOrderRule(order.Status));
        CheckRule(new CannotPayCompletedOrderRule(order.Status));
        CheckRule(new CannotAcceptInsufficientPaymentRule(order.Total, amountReceived));

        var change = amountReceived.Amount - order.Total.Amount;
        var changeMoney = Money.From(change);

        var payment = new Payment(
            PaymentId.New(),
            order.Id,
            order.Total,
            amountReceived,
            changeMoney,
            method);

        payment.RaiseDomainEvent(
            new PaymentReceivedEvent(
                payment.Id,
                order.Id,
                amountReceived,
                changeMoney));

        return payment;
    }

    public void Refund(string? reason = null)
    {
        CheckRule(new CannotRefundNonCompletedPaymentRule(Status));

        Status = PaymentStatus.Refunded;
        _refundReason = reason;

        RaiseDomainEvent(
            new PaymentRefundedEvent(
                Id,
                OrderId,
                AmountReceived,
                reason));
    }
}