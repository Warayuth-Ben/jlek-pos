using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Kitchen.Responses;
using JLek.POS.Domain.Kitchen;

namespace JLek.POS.Application.Features.Kitchen.Commands.CreateKitchenTicket;

public sealed class CreateKitchenTicketCommandHandler
{
    private readonly IKitchenTicketRepository _repository;

    public CreateKitchenTicketCommandHandler(
        IKitchenTicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<KitchenTicketResponse> Handle(
        CreateKitchenTicketCommand command,
        CancellationToken cancellationToken = default)
    {
        var ticket = KitchenTicket.Create(
            command.TicketNumber,
            command.ItemName,
            command.Quantity,
            command.Notes);

        await _repository.AddAsync(
            ticket,
            cancellationToken);

        return KitchenTicketResponse.FromDomain(ticket);
    }
}