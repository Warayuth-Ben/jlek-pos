using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Orders.Rules;

public sealed class CannotCancelNonConfirmedOrderRule : IBusinessRule
{
    private readonly OrderStatus _status;

    public CannotCancelNonConfirmedOrderRule(OrderStatus status)
    {
        _status = status;
    }

    public bool IsBroken()
    {
        return _status != OrderStatus.Confirmed;
    }

    public string Message =>
        "Only confirmed orders can be cancelled.";
}