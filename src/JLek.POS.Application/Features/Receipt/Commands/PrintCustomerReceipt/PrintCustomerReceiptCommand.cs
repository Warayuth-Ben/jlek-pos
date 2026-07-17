namespace JLek.POS.Application.Features.Receipt.Commands.PrintCustomerReceipt;

public sealed record PrintCustomerReceiptCommand(
    Guid OrderId,
    bool IsReprint = false,
    int Copies = 1);