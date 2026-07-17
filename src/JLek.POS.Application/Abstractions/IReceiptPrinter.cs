using JLek.POS.Application.Features.Receipt.Models;

namespace JLek.POS.Application.Abstractions;

public interface IReceiptPrinter
{
    string PrinterName { get; }
    Task<PrintResult> PrintAsync(
        ReceiptDocument document,
        CancellationToken cancellationToken = default);
}

public interface IKitchenPrinter
{
    string PrinterName { get; }
    Task<PrintResult> PrintAsync(
        ReceiptDocument document,
        CancellationToken cancellationToken = default);
}