using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Tables.Rules;

public sealed class CannotAssignOccupiedTableRule : IBusinessRule
{
    private readonly TableStatus _status;

    public CannotAssignOccupiedTableRule(TableStatus status)
    {
        _status = status;
    }

    public string Message =>
        "Cannot assign an occupied table to a new session.";

    public bool IsBroken() =>
        _status == TableStatus.Occupied;
}