using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Tables.Responses;

namespace JLek.POS.Application.Features.Tables.Queries.GetDiningTables;

public sealed class GetDiningTablesQueryHandler
{
    private readonly IDiningTableRepository _repository;

    public GetDiningTablesQueryHandler(
        IDiningTableRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<DiningTableResponse>> Handle(
        GetDiningTablesQuery query,
        CancellationToken cancellationToken = default)
    {
        var tables = await _repository.GetAllAsync(
            cancellationToken);

        return tables
            .Select(DiningTableResponse.FromDomain)
            .ToList();
    }
}