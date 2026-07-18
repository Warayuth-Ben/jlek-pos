using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Tables.Rules;

public sealed class CannotOpenNonAvailableTableRule : IBusinessRule
{
    private readonly TableStatus _status;

    public CannotOpenNonAvailableTableRule(TableStatus status)
    {
        _status = status;
    }

    public string Message =>
        "Cannot open a table that is not available.";

    public bool IsBroken() =>
        _status != TableStatus.Available;
}