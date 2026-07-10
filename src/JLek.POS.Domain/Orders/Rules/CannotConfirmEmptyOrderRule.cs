using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Orders.Rules;

public sealed class CannotConfirmEmptyOrderRule : IBusinessRule
{
    private readonly int _itemCount;

    public CannotConfirmEmptyOrderRule(int itemCount)
    {
        _itemCount = itemCount;
    }

    public bool IsBroken()
    {
        return _itemCount == 0;
    }

    public string Message =>
        "An order must contain at least one item before it can be confirmed.";
}
