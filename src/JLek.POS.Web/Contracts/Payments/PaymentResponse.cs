namespace JLek.POS.Web.Contracts.Payments;

public sealed record PaymentResponse(
    Guid Id,
    Guid OrderId,
    decimal Amount,
    string Method,
    string Status);