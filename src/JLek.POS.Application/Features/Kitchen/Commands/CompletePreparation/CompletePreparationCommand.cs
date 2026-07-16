using JLek.POS.Domain.Kitchen;

namespace JLek.POS.Application.Features.Kitchen.Commands.CompletePreparation;

public sealed record CompletePreparationCommand(
    KitchenTicketId TicketId);