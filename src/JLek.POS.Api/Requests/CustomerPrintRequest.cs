namespace JLek.POS.Api.Requests;

public sealed record CustomerPrintRequest(
    Guid OrderId,
    bool IsReprint = false,
    int Copies = 1);