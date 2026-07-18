using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Kitchen.Responses;
using JLek.POS.Domain.Kitchen;

namespace JLek.POS.Application.Features.Kitchen.Queries.GetActiveKitchenTickets;

public sealed class GetActiveKitchenTicketsQueryHandler
{
    private readonly IKitchenTicketRepository _repository;

    public GetActiveKitchenTicketsQueryHandler(
        IKitchenTicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<KitchenTicketResponse>> Handle(
        GetActiveKitchenTicketsQuery query,
        CancellationToken cancellationToken = default)
    {
        var allTickets = await _repository.GetAllAsync(
            cancellationToken);

        return allTickets
            .Where(t => t.Status != KitchenTicketStatus.Served)
            .OrderBy(t => t.TicketNumber)
            .Select(KitchenTicketResponse.FromDomain)
            .ToList();
    }
}