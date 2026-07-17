using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Payments.Rules;

public sealed class CannotRefundNonCompletedPaymentRule : IBusinessRule
{
    private readonly PaymentStatus _status;

    public CannotRefundNonCompletedPaymentRule(PaymentStatus status)
    {
        _status = status;
    }

    public string Message => "Cannot refund a payment that is not completed.";

    public bool IsBroken()
    {
        return _status != PaymentStatus.Completed;
    }
}