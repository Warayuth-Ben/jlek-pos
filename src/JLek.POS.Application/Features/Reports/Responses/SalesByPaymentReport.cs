namespace JLek.POS.Application.Features.Reports.Responses;

public sealed record SalesByPaymentReport
{
    public DateOnly DateFrom { get; init; }
    public DateOnly DateTo { get; init; }
    public IReadOnlyList<PaymentMethodSummary> PaymentMethods { get; init; } = [];
}

public sealed record PaymentMethodSummary
{
    public string Method { get; init; } = string.Empty;
    public int TransactionCount { get; init; }
    public decimal TotalAmount { get; init; }
}