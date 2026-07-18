using JLek.POS.Domain.Kitchen;

namespace JLek.POS.Application.Features.Kitchen.Queries.GetKitchenTicketById;

public sealed record GetKitchenTicketByIdQuery(
    KitchenTicketId TicketId);