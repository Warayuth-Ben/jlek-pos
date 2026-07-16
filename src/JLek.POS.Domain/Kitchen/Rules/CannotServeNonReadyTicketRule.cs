using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Kitchen.Rules;

public sealed class CannotServeNonReadyTicketRule : IBusinessRule
{
    private readonly KitchenTicketStatus _status;

    public CannotServeNonReadyTicketRule(KitchenTicketStatus status)
    {
        _status = status;
    }

    public string Message =>
        "Cannot serve a ticket that is not in Ready status.";

    public bool IsBroken() =>
        _status != KitchenTicketStatus.Ready;
}