using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Orders.Rules;

public sealed class CannotCompleteNonConfirmedOrderRule : IBusinessRule
{
    private readonly OrderStatus _status;

    public CannotCompleteNonConfirmedOrderRule(OrderStatus status)
    {
        _status = status;
    }

    public bool IsBroken()
    {
        return _status != OrderStatus.Confirmed;
    }

    public string Message =>
        "Only confirmed orders can be completed.";
}
