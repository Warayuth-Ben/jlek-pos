using JLek.POS.Domain.Kitchen;

namespace JLek.POS.Application.Features.Kitchen.Responses;

public record KitchenTicketResponse(
    KitchenTicketId Id,
    int TicketNumber,
    KitchenTicketStatus Status,
    IReadOnlyList<KitchenItemResponse> Items)
{
    public static KitchenTicketResponse FromDomain(KitchenTicket ticket)
    {
        return new KitchenTicketResponse(
            ticket.Id,
            ticket.TicketNumber,
            ticket.Status,
            ticket.Items
                .Select(KitchenItemResponse.FromDomain)
                .ToList());
    }
}