using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Kitchen.Responses;

namespace JLek.POS.Application.Features.Kitchen.Commands.ServeKitchenTicket;

public sealed class ServeKitchenTicketCommandHandler
{
    private readonly IKitchenTicketRepository _repository;

    public ServeKitchenTicketCommandHandler(
        IKitchenTicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<KitchenTicketResponse> Handle(
        ServeKitchenTicketCommand command,
        CancellationToken cancellationToken = default)
    {
        var ticket = await _repository.GetByIdAsync(
            command.TicketId,
            cancellationToken);

        if (ticket is null)
        {
            throw new InvalidOperationException("Kitchen ticket not found.");
        }

        ticket.Serve();

        await _repository.UpdateAsync(
            ticket,
            cancellationToken);

        return KitchenTicketResponse.FromDomain(ticket);
    }
}