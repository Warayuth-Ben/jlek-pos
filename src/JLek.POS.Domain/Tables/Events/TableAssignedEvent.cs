using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Domain.Tables.Events;

public sealed class TableAssignedEvent : DomainEvent
{
    public TableAssignedEvent(
        TableId tableId,
        OrderSessionId sessionId)
    {
        TableId = tableId;
        SessionId = sessionId;
    }

    public TableId TableId { get; }

    public OrderSessionId SessionId { get; }
}