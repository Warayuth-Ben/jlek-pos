using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Domain.Tables.Events;

public sealed class TablesSplitEvent : DomainEvent
{
    public TablesSplitEvent(
        TableId primaryTableId,
        IReadOnlyCollection<TableId> splitTableIds)
    {
        PrimaryTableId = primaryTableId;
        SplitTableIds = splitTableIds;
    }

    public TableId PrimaryTableId { get; }

    public IReadOnlyCollection<TableId> SplitTableIds { get; }
}