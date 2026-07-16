using JLek.POS.Domain.Orders.ValueObjects;
using JLek.POS.Domain.Tables;
using JLek.POS.Domain.ValueObjects;

namespace JLek.POS.Application.Features.Tables.Responses;

public record DiningTableResponse(
    TableId Id,
    string Name,
    TableStatus Status,
    OrderSessionId? ActiveSessionId,
    IReadOnlyCollection<TableId> MergedTableIds)
{
    public static DiningTableResponse FromDomain(DiningTable table)
    {
        return new DiningTableResponse(
            table.Id,
            table.Name,
            table.Status,
            table.ActiveSessionId,
            table.MergedTableIds);
    }
}