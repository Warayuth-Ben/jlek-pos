using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Tables.Responses;

namespace JLek.POS.Application.Features.Tables.Commands.AssignTable;

public sealed class AssignTableCommandHandler
{
    private readonly IDiningTableRepository _repository;

    public AssignTableCommandHandler(
        IDiningTableRepository repository)
    {
        _repository = repository;
    }

    public async Task<DiningTableResponse> Handle(
        AssignTableCommand command,
        CancellationToken cancellationToken = default)
    {
        var table = await _repository.GetByIdAsync(
            command.TableId,
            cancellationToken);

        if (table is null)
        {
            throw new InvalidOperationException("Dining table not found.");
        }

        table.Assign(command.SessionId);

        await _repository.UpdateAsync(
            table,
            cancellationToken);

        return DiningTableResponse.FromDomain(table);
    }
}