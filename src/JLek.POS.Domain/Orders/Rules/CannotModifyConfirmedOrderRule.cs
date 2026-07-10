using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Orders.Rules;

public sealed class CannotModifyConfirmedOrderRule : IBusinessRule
{
    private readonly OrderStatus _status;

    public CannotModifyConfirmedOrderRule(OrderStatus status)
    {
        _status = status;
    }

    public bool IsBroken()
    {
        return _status == OrderStatus.Confirmed;
    }

    public string Message =>
        "Confirmed orders cannot be structurally modified.";
}
