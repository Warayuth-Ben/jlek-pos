namespace JLek.POS.Api.Requests;

public sealed record KitchenPrintRequest(
    int TicketNumber,
    int Copies = 1);