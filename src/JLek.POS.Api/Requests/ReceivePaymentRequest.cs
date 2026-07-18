namespace JLek.POS.Api.Requests;

public sealed record ReceivePaymentRequest(
    Guid OrderId,
    decimal AmountReceived,
    int Method);