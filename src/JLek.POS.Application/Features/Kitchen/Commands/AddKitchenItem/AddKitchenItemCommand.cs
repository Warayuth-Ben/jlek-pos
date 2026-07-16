using JLek.POS.Domain.Kitchen;

namespace JLek.POS.Application.Features.Kitchen.Commands.AddKitchenItem;

public sealed record AddKitchenItemCommand(
    KitchenTicketId TicketId,
    string ItemName,
    int Quantity,
    string? Notes);