using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Tables.Responses;
using JLek.POS.Domain.Tables;

namespace JLek.POS.Application.Features.Tables.Queries.GetAvailableDiningTables;

public sealed class GetAvailableDiningTablesQueryHandler
{
    private readonly IDiningTableRepository _repository;

    public GetAvailableDiningTablesQueryHandler(
        IDiningTableRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<DiningTableResponse>> Handle(
        GetAvailableDiningTablesQuery query,
        CancellationToken cancellationToken = default)
    {
        var allTables = await _repository.GetAllAsync(
            cancellationToken);

        return allTables
            .Where(t => t.Status == TableStatus.Available)
            .Select(DiningTableResponse.FromDomain)
            .ToList();
    }
}