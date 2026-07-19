using JLek.POS.Domain.Kitchen;

namespace JLek.POS.Application.Features.Kitchen.Responses;

public record KitchenTicketResponse(
    Guid Id,
    int TicketNumber,
    string Status,
    IReadOnlyList<KitchenItemResponse> Items)
{
    public static KitchenTicketResponse FromDomain(KitchenTicket ticket)
    {
        return new KitchenTicketResponse(
            ticket.Id.Value,
            ticket.TicketNumber,
            ticket.Status.ToString(),
            ticket.Items
                .Select(KitchenItemResponse.FromDomain)
                .ToList());
    }
}