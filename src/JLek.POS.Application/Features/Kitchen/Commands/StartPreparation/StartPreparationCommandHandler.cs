using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Kitchen.Responses;

namespace JLek.POS.Application.Features.Kitchen.Commands.StartPreparation;

public sealed class StartPreparationCommandHandler
{
    private readonly IKitchenTicketRepository _repository;

    public StartPreparationCommandHandler(
        IKitchenTicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<KitchenTicketResponse> Handle(
        StartPreparationCommand command,
        CancellationToken cancellationToken = default)
    {
        var ticket = await _repository.GetByIdAsync(
            command.TicketId,
            cancellationToken);

        if (ticket is null)
        {
            throw new InvalidOperationException("Kitchen ticket not found.");
        }

        ticket.StartPreparation();

        await _repository.UpdateAsync(
            ticket,
            cancellationToken);

        return KitchenTicketResponse.FromDomain(ticket);
    }
}