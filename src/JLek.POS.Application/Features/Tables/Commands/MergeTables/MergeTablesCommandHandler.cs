using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Tables.Responses;

namespace JLek.POS.Application.Features.Tables.Commands.MergeTables;

public sealed class MergeTablesCommandHandler
{
    private readonly IDiningTableRepository _repository;

    public MergeTablesCommandHandler(
        IDiningTableRepository repository)
    {
        _repository = repository;
    }

    public async Task<DiningTableResponse> Handle(
        MergeTablesCommand command,
        CancellationToken cancellationToken = default)
    {
        var primaryTable = await _repository.GetByIdAsync(
            command.PrimaryTableId,
            cancellationToken);

        if (primaryTable is null)
        {
            throw new InvalidOperationException("Primary dining table not found.");
        }

        var tableToMerge = await _repository.GetByIdAsync(
            command.TableToMergeId,
            cancellationToken);

        if (tableToMerge is null)
        {
            throw new InvalidOperationException("Table to merge not found.");
        }

        primaryTable.Merge(tableToMerge);

        await _repository.UpdateAsync(
            primaryTable,
            cancellationToken);

        await _repository.UpdateAsync(
            tableToMerge,
            cancellationToken);

        return DiningTableResponse.FromDomain(primaryTable);
    }
}