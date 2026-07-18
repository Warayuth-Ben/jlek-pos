using JLek.POS.Domain.Common.Rules;
using JLek.POS.Domain.Common.ValueObjects;

namespace JLek.POS.Domain.Payments.Rules;

public sealed class CannotAcceptInsufficientPaymentRule : IBusinessRule
{
    private readonly Money _orderTotal;
    private readonly Money _amountReceived;

    public CannotAcceptInsufficientPaymentRule(
        Money orderTotal,
        Money amountReceived)
    {
        _orderTotal = orderTotal;
        _amountReceived = amountReceived;
    }

    public string Message =>
        $"Amount received ({_amountReceived.Amount:N2}) is less than order total ({_orderTotal.Amount:N2}).";

    public bool IsBroken()
    {
        return _amountReceived.Amount < _orderTotal.Amount;
    }
}