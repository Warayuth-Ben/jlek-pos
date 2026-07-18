using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Tables.Responses;

namespace JLek.POS.Application.Features.Tables.Commands.OpenTable;

public sealed class OpenTableCommandHandler
{
    private readonly IDiningTableRepository _repository;

    public OpenTableCommandHandler(
        IDiningTableRepository repository)
    {
        _repository = repository;
    }

    public async Task<DiningTableResponse> Handle(
        OpenTableCommand command,
        CancellationToken cancellationToken = default)
    {
        var table = await _repository.GetByIdAsync(
            command.TableId,
            cancellationToken);

        if (table is null)
        {
            throw new InvalidOperationException("Dining table not found.");
        }

        table.Open();

        await _repository.UpdateAsync(
            table,
            cancellationToken);

        return DiningTableResponse.FromDomain(table);
    }
}