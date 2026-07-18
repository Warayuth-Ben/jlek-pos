using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Domain.Tables.Events;

public sealed class TableOpenedEvent : DomainEvent
{
    public TableOpenedEvent(TableId tableId)
    {
        TableId = tableId;
    }

    public TableId TableId { get; }
}