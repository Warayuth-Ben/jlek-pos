using JLek.POS.Application.Abstractions;
using JLek.POS.Application.Features.Receipt.Models;

namespace JLek.POS.Infrastructure.Printing;

public sealed class NullReceiptPrinter : IReceiptPrinter
{
    public string PrinterName => "Null Printer (Development)";

    public Task<PrintResult> PrintAsync(
        ReceiptDocument document,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new PrintResult
        {
            Success = true,
            StartedAt = DateTime.UtcNow,
            FinishedAt = DateTime.UtcNow,
            Status = "Completed",
            PrinterName = PrinterName,
            Copies = 1
        });
    }
}

public sealed class NullKitchenPrinter : IKitchenPrinter
{
    public string PrinterName => "Null Kitchen Printer (Development)";

    public Task<PrintResult> PrintAsync(
        ReceiptDocument document,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new PrintResult
        {
            Success = true,
            StartedAt = DateTime.UtcNow,
            FinishedAt = DateTime.UtcNow,
            Status = "Completed",
            PrinterName = PrinterName,
            Copies = 1
        });
    }
}