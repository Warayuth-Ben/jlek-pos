using JLek.POS.Domain.Common.Rules;

namespace JLek.POS.Domain.Kitchen.Rules;

public sealed class CannotModifyServedTicketRule : IBusinessRule
{
    private readonly KitchenTicketStatus _status;

    public CannotModifyServedTicketRule(KitchenTicketStatus status)
    {
        _status = status;
    }

    public string Message =>
        "Cannot modify a served kitchen ticket.";

    public bool IsBroken() =>
        _status == KitchenTicketStatus.Served;
}