using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Tables.Responses;

namespace JLek.POS.Application.Features.Tables.Commands.ReleaseTable;

public sealed class ReleaseTableCommandHandler
{
    private readonly IDiningTableRepository _repository;

    public ReleaseTableCommandHandler(
        IDiningTableRepository repository)
    {
        _repository = repository;
    }

    public async Task<DiningTableResponse> Handle(
        ReleaseTableCommand command,
        CancellationToken cancellationToken = default)
    {
        var table = await _repository.GetByIdAsync(
            command.TableId,
            cancellationToken);

        if (table is null)
        {
            throw new InvalidOperationException("Dining table not found.");
        }

        table.Release();

        await _repository.UpdateAsync(
            table,
            cancellationToken);

        return DiningTableResponse.FromDomain(table);
    }
}