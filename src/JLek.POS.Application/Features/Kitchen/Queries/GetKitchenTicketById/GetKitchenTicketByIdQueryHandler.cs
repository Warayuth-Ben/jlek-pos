using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Kitchen.Responses;

namespace JLek.POS.Application.Features.Kitchen.Queries.GetKitchenTicketById;

public sealed class GetKitchenTicketByIdQueryHandler
{
    private readonly IKitchenTicketRepository _repository;

    public GetKitchenTicketByIdQueryHandler(
        IKitchenTicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<KitchenTicketResponse?> Handle(
        GetKitchenTicketByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var ticket = await _repository.GetByIdAsync(
            query.TicketId,
            cancellationToken);

        return ticket is not null
            ? KitchenTicketResponse.FromDomain(ticket)
            : null;
    }
}