using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Kitchen.Events;

public sealed class KitchenPreparationCompletedEvent : DomainEvent
{
    public KitchenPreparationCompletedEvent(
        KitchenTicketId ticketId)
    {
        TicketId = ticketId;
    }

    public KitchenTicketId TicketId { get; }
}