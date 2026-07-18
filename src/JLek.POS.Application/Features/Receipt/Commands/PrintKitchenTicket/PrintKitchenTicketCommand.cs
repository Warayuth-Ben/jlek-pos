namespace JLek.POS.Application.Features.Receipt.Commands.PrintKitchenTicket;

public sealed record PrintKitchenTicketCommand(
    int TicketNumber,
    int Copies = 1);