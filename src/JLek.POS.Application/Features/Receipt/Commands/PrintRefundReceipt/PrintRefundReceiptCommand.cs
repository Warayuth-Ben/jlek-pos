namespace JLek.POS.Application.Features.Receipt.Commands.PrintRefundReceipt;

public sealed record PrintRefundReceiptCommand(
    Guid PaymentId,
    bool IsReprint = false,
    int Copies = 1);