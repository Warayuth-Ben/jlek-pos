using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Tables.Responses;
using JLek.POS.Domain.Tables;

namespace JLek.POS.Application.Features.Tables.Commands.CreateDiningTable;

public sealed class CreateDiningTableCommandHandler
{
    private readonly IDiningTableRepository _repository;

    public CreateDiningTableCommandHandler(
        IDiningTableRepository repository)
    {
        _repository = repository;
    }

    public async Task<DiningTableResponse> Handle(
        CreateDiningTableCommand command,
        CancellationToken cancellationToken = default)
    {
        var table = DiningTable.Create(command.Name);

        await _repository.AddAsync(
            table,
            cancellationToken);

        return DiningTableResponse.FromDomain(table);
    }
}