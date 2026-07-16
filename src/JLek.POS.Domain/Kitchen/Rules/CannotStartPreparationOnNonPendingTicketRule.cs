using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Kitchen.Rules;

public sealed class CannotStartPreparationOnNonPendingTicketRule : IBusinessRule
{
    private readonly KitchenTicketStatus _status;

    public CannotStartPreparationOnNonPendingTicketRule(KitchenTicketStatus status)
    {
        _status = status;
    }

    public string Message =>
        "Cannot start preparation on a ticket that is not in Pending status.";

    public bool IsBroken() =>
        _status != KitchenTicketStatus.Pending;
}