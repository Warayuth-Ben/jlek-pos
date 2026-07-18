using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Tables.Responses;

namespace JLek.POS.Application.Features.Tables.Commands.TransferTable;

public sealed class TransferTableCommandHandler
{
    private readonly IDiningTableRepository _repository;

    public TransferTableCommandHandler(
        IDiningTableRepository repository)
    {
        _repository = repository;
    }

    public async Task<DiningTableResponse> Handle(
        TransferTableCommand command,
        CancellationToken cancellationToken = default)
    {
        var sourceTable = await _repository.GetByIdAsync(
            command.SourceTableId,
            cancellationToken);

        if (sourceTable is null)
        {
            throw new InvalidOperationException("Source dining table not found.");
        }

        var destinationTable = await _repository.GetByIdAsync(
            command.DestinationTableId,
            cancellationToken);

        if (destinationTable is null)
        {
            throw new InvalidOperationException("Destination dining table not found.");
        }

        sourceTable.TransferTo(destinationTable);

        await _repository.UpdateAsync(
            sourceTable,
            cancellationToken);

        await _repository.UpdateAsync(
            destinationTable,
            cancellationToken);

        return DiningTableResponse.FromDomain(sourceTable);
    }
}