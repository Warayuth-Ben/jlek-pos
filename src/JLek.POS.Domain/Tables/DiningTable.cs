using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.Tables.Events;
using JLek.POS.Domain.Tables.Rules;
using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Domain.Tables;

public sealed class DiningTable : AggregateRoot<TableId>
{
    private readonly List<TableId> _mergedTableIds = [];

    private DiningTable()
        : base(TableId.From(Guid.Empty))
    {
        Name = string.Empty;
    }

    private DiningTable(
        TableId id,
        string name)
        : base(id)
    {
        Name = name;
        Status = TableStatus.Available;
    }

    public string Name { get; private set; }

    public TableStatus Status { get; private set; }

    public OrderSessionId? ActiveSessionId { get; private set; }

    public IReadOnlyCollection<TableId> MergedTableIds =>
        _mergedTableIds.AsReadOnly();

    public void Open()
    {
        CheckRule(new CannotOpenNonAvailableTableRule(Status));

        Status = TableStatus.Open;

        RaiseDomainEvent(
            new TableOpenedEvent(Id));
    }

    public static DiningTable Create(string name)
    {
        var table = new DiningTable(
            TableId.New(),
            name);

        return table;
    }

    public void Assign(OrderSessionId sessionId)
    {
        CheckRule(new CannotAssignOccupiedTableRule(Status));

        ActiveSessionId = sessionId;
        Status = TableStatus.Occupied;

        RaiseDomainEvent(
            new TableAssignedEvent(Id, sessionId));
    }

    public void TransferTo(DiningTable destinationTable)
    {
        CheckRule(new CannotAssignOccupiedTableRule(Status));

        if (ActiveSessionId is null)
        {
            return;
        }

        if (destinationTable.Status == TableStatus.Occupied)
        {
            CheckRule(
                new CannotTransferToOccupiedTableRule(destinationTable.Status));
        }

        var sessionId = ActiveSessionId;

        // Release source table
        ActiveSessionId = null;
        Status = TableStatus.Available;

        // Occupy destination table
        destinationTable.ActiveSessionId = sessionId;
        destinationTable.Status = TableStatus.Occupied;

        RaiseDomainEvent(
            new TableTransferredEvent(Id, destinationTable.Id, sessionId));
    }

    public void Merge(DiningTable tableToMerge)
    {
        CheckRule(new CannotAssignOccupiedTableRule(Status));
        CheckRule(new CannotAssignOccupiedTableRule(tableToMerge.Status));

        if (ActiveSessionId is null)
        {
            return;
        }

        _mergedTableIds.Add(tableToMerge.Id);

        tableToMerge.Status = TableStatus.Occupied;

        RaiseDomainEvent(
            new TablesMergedEvent(Id, [tableToMerge.Id], ActiveSessionId));
    }

    public void Split(TableId tableId)
    {
        var mergedTable = _mergedTableIds.FirstOrDefault(x => x == tableId);

        if (mergedTable is null)
        {
            return;
        }

        _mergedTableIds.Remove(mergedTable);

        RaiseDomainEvent(
            new TablesSplitEvent(Id, [tableId]));
    }

    public void Release()
    {
        CheckRule(new CannotReleaseAvailableTableRule(Status));

        if (ActiveSessionId is null)
        {
            return;
        }

        var sessionId = ActiveSessionId;

        ActiveSessionId = null;
        Status = TableStatus.Available;
        _mergedTableIds.Clear();

        RaiseDomainEvent(
            new TableReleasedEvent(Id, sessionId));
    }
}