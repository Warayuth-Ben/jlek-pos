using JLek.POS.Application.Features.Receipt.Models;
using JLek.POS.Printing.Abstractions;
using JLek.POS.Printing.Models;

namespace JLek.POS.Printing.Infrastructure;

public sealed class NullPrinterAdapter : IPrinterAdapter
{
    public Task<PrintResult> PrintAsync(
        PrintPayload payload,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        return Task.FromResult(new PrintResult
        {
            Success = true,
            StartedAt = now,
            FinishedAt = now,
            Status = "Completed",
            PrinterName = "Null Printer (Development)",
            Copies = 1
        });
    }

    public Task<PrinterStatus> GetStatusAsync()
    {
        return Task.FromResult(new PrinterStatus
        {
            IsOnline = true,
            HasPaper = true,
            IsBusy = false
        });
    }
}