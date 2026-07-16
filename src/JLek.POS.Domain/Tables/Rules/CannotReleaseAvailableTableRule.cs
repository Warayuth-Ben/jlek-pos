using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Tables.Rules;

public sealed class CannotReleaseAvailableTableRule : IBusinessRule
{
    private readonly TableStatus _status;

    public CannotReleaseAvailableTableRule(TableStatus status)
    {
        _status = status;
    }

    public string Message =>
        "Cannot release an available table.";

    public bool IsBroken() =>
        _status == TableStatus.Available;
}