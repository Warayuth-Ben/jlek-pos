using JLek.POS.Domain.Common.Primitives;

namespace JLek.POS.Domain.Kitchen.Events;

public sealed class KitchenTicketCreatedEvent : DomainEvent
{
    public KitchenTicketCreatedEvent(
        KitchenTicketId ticketId,
        int ticketNumber)
    {
        TicketId = ticketId;
        TicketNumber = ticketNumber;
    }

    public KitchenTicketId TicketId { get; }

    public int TicketNumber { get; }
}