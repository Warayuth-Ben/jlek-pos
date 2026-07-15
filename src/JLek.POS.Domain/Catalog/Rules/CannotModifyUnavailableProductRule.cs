using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Catalog.Rules;

public sealed class CannotModifyUnavailableProductRule : IBusinessRule
{
    private readonly ProductStatus _status;

    public CannotModifyUnavailableProductRule(ProductStatus status)
    {
        _status = status;
    }

    public bool IsBroken()
    {
        return _status == ProductStatus.Unavailable;
    }

    public string Message =>
        "Unavailable products cannot be modified.";
}