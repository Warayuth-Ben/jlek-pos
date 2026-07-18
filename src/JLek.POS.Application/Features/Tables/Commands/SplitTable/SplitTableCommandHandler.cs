using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Tables.Responses;

namespace JLek.POS.Application.Features.Tables.Commands.SplitTable;

public sealed class SplitTableCommandHandler
{
    private readonly IDiningTableRepository _repository;

    public SplitTableCommandHandler(
        IDiningTableRepository repository)
    {
        _repository = repository;
    }

    public async Task<DiningTableResponse> Handle(
        SplitTableCommand command,
        CancellationToken cancellationToken = default)
    {
        var primaryTable = await _repository.GetByIdAsync(
            command.PrimaryTableId,
            cancellationToken);

        if (primaryTable is null)
        {
            throw new InvalidOperationException("Primary dining table not found.");
        }

        primaryTable.Split(command.TableToSplitId);

        await _repository.UpdateAsync(
            primaryTable,
            cancellationToken);

        return DiningTableResponse.FromDomain(primaryTable);
    }
}