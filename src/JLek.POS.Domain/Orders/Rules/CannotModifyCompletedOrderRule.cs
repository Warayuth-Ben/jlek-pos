using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Orders.Rules;

public sealed class CannotModifyCompletedOrderRule : IBusinessRule
{
    private readonly OrderStatus _status;

    public CannotModifyCompletedOrderRule(OrderStatus status)
    {
        _status = status;
    }

    public bool IsBroken()
    {
        return _status == OrderStatus.Completed;
    }

    public string Message =>
        "Completed orders cannot be structurally modified.";
}