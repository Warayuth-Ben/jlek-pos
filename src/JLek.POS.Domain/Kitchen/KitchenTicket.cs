using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Kitchen.Events;
using JLek.POS.Domain.Kitchen.Rules;

namespace JLek.POS.Domain.Kitchen;

public sealed class KitchenTicket : AggregateRoot<KitchenTicketId>
{
    private readonly List<KitchenItem> _items = [];

    private KitchenTicket()
        : base(KitchenTicketId.From(Guid.Empty))
    {
    }

    private KitchenTicket(
        KitchenTicketId id,
        int ticketNumber,
        KitchenTicketStatus status)
        : base(id)
    {
        TicketNumber = ticketNumber;
        Status = status;
    }

    public int TicketNumber { get; private set; }

    public KitchenTicketStatus Status { get; private set; }

    public IReadOnlyCollection<KitchenItem> Items =>
        _items.AsReadOnly();

    public static KitchenTicket Create(
        int ticketNumber,
        string itemName,
        int quantity,
        string? notes)
    {
        var ticket = new KitchenTicket(
            KitchenTicketId.New(),
            ticketNumber,
            KitchenTicketStatus.Pending);

        var item = KitchenItem.Create(
            itemName,
            quantity,
            notes);

        ticket._items.Add(item);

        ticket.RaiseDomainEvent(
            new KitchenTicketCreatedEvent(ticket.Id, ticket.TicketNumber));

        return ticket;
    }

    public void AddItem(
        string itemName,
        int quantity,
        string? notes)
    {
        CheckRule(new CannotModifyServedTicketRule(Status));

        var item = KitchenItem.Create(
            itemName,
            quantity,
            notes);

        _items.Add(item);
    }

    public void StartPreparation()
    {
        CheckRule(new CannotModifyServedTicketRule(Status));
        CheckRule(new CannotStartPreparationOnNonPendingTicketRule(Status));

        Status = KitchenTicketStatus.Preparing;

        RaiseDomainEvent(
            new KitchenPreparationStartedEvent(Id));
    }

    public void CompletePreparation()
    {
        CheckRule(new CannotModifyServedTicketRule(Status));
        CheckRule(new CannotCompletePreparationOnNonPreparingTicketRule(Status));

        Status = KitchenTicketStatus.Ready;

        RaiseDomainEvent(
            new KitchenPreparationCompletedEvent(Id));
    }

    public void Serve()
    {
        CheckRule(new CannotModifyServedTicketRule(Status));
        CheckRule(new CannotServeNonReadyTicketRule(Status));

        Status = KitchenTicketStatus.Served;

        RaiseDomainEvent(
            new KitchenItemsServedEvent(Id));
    }
}