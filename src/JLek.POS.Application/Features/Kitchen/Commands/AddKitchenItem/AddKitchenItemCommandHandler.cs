using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Kitchen.Responses;

namespace JLek.POS.Application.Features.Kitchen.Commands.AddKitchenItem;

public sealed class AddKitchenItemCommandHandler
{
    private readonly IKitchenTicketRepository _repository;

    public AddKitchenItemCommandHandler(
        IKitchenTicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<KitchenTicketResponse> Handle(
        AddKitchenItemCommand command,
        CancellationToken cancellationToken = default)
    {
        var ticket = await _repository.GetByIdAsync(
            command.TicketId,
            cancellationToken);

        if (ticket is null)
        {
            throw new InvalidOperationException("Kitchen ticket not found.");
        }

        ticket.AddItem(
            command.ItemName,
            command.Quantity,
            command.Notes);

        await _repository.UpdateAsync(
            ticket,
            cancellationToken);

        return KitchenTicketResponse.FromDomain(ticket);
    }
}