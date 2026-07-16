using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Domain.Tables.Events;

public sealed class TablesMergedEvent : DomainEvent
{
    public TablesMergedEvent(
        TableId primaryTableId,
        IReadOnlyCollection<TableId> mergedTableIds,
        OrderSessionId sessionId)
    {
        PrimaryTableId = primaryTableId;
        MergedTableIds = mergedTableIds;
        SessionId = sessionId;
    }

    public TableId PrimaryTableId { get; }

    public IReadOnlyCollection<TableId> MergedTableIds { get; }

    public OrderSessionId SessionId { get; }
}