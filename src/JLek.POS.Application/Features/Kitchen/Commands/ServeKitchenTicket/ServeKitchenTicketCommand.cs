using JLek.POS.Domain.Kitchen;

namespace JLek.POS.Application.Features.Kitchen.Commands.ServeKitchenTicket;

public sealed record ServeKitchenTicketCommand(
    KitchenTicketId TicketId);