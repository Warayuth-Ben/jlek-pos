using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Kitchen.Events;

public sealed class KitchenPreparationStartedEvent : DomainEvent
{
    public KitchenPreparationStartedEvent(
        KitchenTicketId ticketId)
    {
        TicketId = ticketId;
    }

    public KitchenTicketId TicketId { get; }
}