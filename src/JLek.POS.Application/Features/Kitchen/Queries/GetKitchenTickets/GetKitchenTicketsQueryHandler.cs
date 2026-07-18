using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Kitchen.Responses;

namespace JLek.POS.Application.Features.Kitchen.Queries.GetKitchenTickets;

public sealed class GetKitchenTicketsQueryHandler
{
    private readonly IKitchenTicketRepository _repository;

    public GetKitchenTicketsQueryHandler(
        IKitchenTicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<KitchenTicketResponse>> Handle(
        GetKitchenTicketsQuery query,
        CancellationToken cancellationToken = default)
    {
        var tickets = await _repository.GetAllAsync(
            cancellationToken);

        return tickets
            .Select(KitchenTicketResponse.FromDomain)
            .ToList();
    }
}