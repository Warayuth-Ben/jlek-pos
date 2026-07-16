using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Domain.Tables.Events;

public sealed class TableTransferredEvent : DomainEvent
{
    public TableTransferredEvent(
        TableId sourceTableId,
        TableId destinationTableId,
        OrderSessionId sessionId)
    {
        SourceTableId = sourceTableId;
        DestinationTableId = destinationTableId;
        SessionId = sessionId;
    }

    public TableId SourceTableId { get; }

    public TableId DestinationTableId { get; }

    public OrderSessionId SessionId { get; }
}