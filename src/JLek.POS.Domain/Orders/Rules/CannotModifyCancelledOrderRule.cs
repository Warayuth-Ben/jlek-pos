using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Orders.Rules;

public sealed class CannotModifyCancelledOrderRule : IBusinessRule
{
    private readonly OrderStatus _status;

    public CannotModifyCancelledOrderRule(OrderStatus status)
    {
        _status = status;
    }

    public bool IsBroken()
    {
        return _status == OrderStatus.Cancelled;
    }

    public string Message =>
        "Cancelled orders cannot be structurally modified.";
}
