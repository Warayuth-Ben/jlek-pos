using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Kitchen.Responses;

namespace JLek.POS.Application.Features.Kitchen.Commands.CompletePreparation;

public sealed class CompletePreparationCommandHandler
{
    private readonly IKitchenTicketRepository _repository;

    public CompletePreparationCommandHandler(
        IKitchenTicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<KitchenTicketResponse> Handle(
        CompletePreparationCommand command,
        CancellationToken cancellationToken = default)
    {
        var ticket = await _repository.GetByIdAsync(
            command.TicketId,
            cancellationToken);

        if (ticket is null)
        {
            throw new InvalidOperationException("Kitchen ticket not found.");
        }

        ticket.CompletePreparation();

        await _repository.UpdateAsync(
            ticket,
            cancellationToken);

        return KitchenTicketResponse.FromDomain(ticket);
    }
}