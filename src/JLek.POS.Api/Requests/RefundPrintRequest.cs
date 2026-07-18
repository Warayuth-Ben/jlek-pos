namespace JLek.POS.Api.Requests;

public sealed record RefundPrintRequest(
    Guid PaymentId,
    bool IsReprint = false,
    int Copies = 1);