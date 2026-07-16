namespace JLek.POS.Application.Features.Kitchen.Commands.CreateKitchenTicket;

public sealed record CreateKitchenTicketCommand(
    int TicketNumber,
    string ItemName,
    int Quantity,
    string? Notes);