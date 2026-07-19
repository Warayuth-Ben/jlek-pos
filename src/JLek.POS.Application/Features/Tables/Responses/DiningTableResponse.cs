using JLek.POS.Domain.Tables;

namespace JLek.POS.Application.Features.Tables.Responses;

public record DiningTableResponse(
    Guid Id,
    string Name,
    string Status,
    Guid? ActiveSessionId,
    List<Guid> MergedTableIds)
{
    public static DiningTableResponse FromDomain(DiningTable table)
    {
        return new DiningTableResponse(
            table.Id.Value,
            table.Name,
            table.Status.ToString(),
            table.ActiveSessionId?.Value,
            table.MergedTableIds.Select(id => id.Value).ToList());
    }
}