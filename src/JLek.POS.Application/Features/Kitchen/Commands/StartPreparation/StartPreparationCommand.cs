using JLek.POS.Domain.Kitchen;

namespace JLek.POS.Application.Features.Kitchen.Commands.StartPreparation;

public sealed record StartPreparationCommand(
    KitchenTicketId TicketId);