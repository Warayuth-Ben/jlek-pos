using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Tables.Rules;

public sealed class CannotTransferToOccupiedTableRule : IBusinessRule
{
    private readonly TableStatus _status;

    public CannotTransferToOccupiedTableRule(TableStatus status)
    {
        _status = status;
    }

    public string Message =>
        "Cannot transfer to an occupied table.";

    public bool IsBroken() =>
        _status == TableStatus.Occupied;
}