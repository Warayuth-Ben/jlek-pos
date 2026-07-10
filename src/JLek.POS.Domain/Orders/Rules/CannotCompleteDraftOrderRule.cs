using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Orders.Rules;

public sealed class CannotCompleteDraftOrderRule : IBusinessRule
{
    private readonly OrderStatus _status;

    public CannotCompleteDraftOrderRule(OrderStatus status)
    {
        _status = status;
    }

    public bool IsBroken()
    {
        return _status == OrderStatus.Draft;
    }

    public string Message =>
        "A draft order cannot be completed.";
}
