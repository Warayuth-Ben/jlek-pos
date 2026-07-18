using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Tables.Responses;

namespace JLek.POS.Application.Features.Tables.Queries.GetDiningTableById;

public sealed class GetDiningTableByIdQueryHandler
{
    private readonly IDiningTableRepository _repository;

    public GetDiningTableByIdQueryHandler(
        IDiningTableRepository repository)
    {
        _repository = repository;
    }

    public async Task<DiningTableResponse?> Handle(
        GetDiningTableByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var table = await _repository.GetByIdAsync(
            query.TableId,
            cancellationToken);

        return table is not null
            ? DiningTableResponse.FromDomain(table)
            : null;
    }
}