using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Orders.Rules;

public sealed class CannotConfirmNonDraftOrderRule : IBusinessRule
{
    private readonly OrderStatus _status;

    public CannotConfirmNonDraftOrderRule(OrderStatus status)
    {
        _status = status;
    }

    public bool IsBroken()
    {
        return _status != OrderStatus.Draft;
    }

    public string Message =>
        "Only draft orders can be confirmed.";
}
