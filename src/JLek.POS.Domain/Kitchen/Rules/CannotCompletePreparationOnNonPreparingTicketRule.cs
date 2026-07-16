using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Kitchen.Rules;

public sealed class CannotCompletePreparationOnNonPreparingTicketRule : IBusinessRule
{
    private readonly KitchenTicketStatus _status;

    public CannotCompletePreparationOnNonPreparingTicketRule(KitchenTicketStatus status)
    {
        _status = status;
    }

    public string Message =>
        "Cannot complete preparation on a ticket that is not in Preparing status.";

    public bool IsBroken() =>
        _status != KitchenTicketStatus.Preparing;
}