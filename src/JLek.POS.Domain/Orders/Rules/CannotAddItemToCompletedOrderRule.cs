using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Orders.Rules;

public sealed class CannotAddItemToCompletedOrderRule : IBusinessRule
{
    private readonly OrderStatus _status;

    public CannotAddItemToCompletedOrderRule(OrderStatus status)
    {
        _status = status;
    }

    public bool IsBroken()
    {
        return _status == OrderStatus.Completed;
    }

    public string Message =>
        "Cannot add items to a completed order.";
}
