using JLek.POS.Application.Features.Receipt.Models;
using JLek.POS.Printing.Models;

namespace JLek.POS.Printing.Abstractions;

public interface IPrinterAdapter
{
    Task<PrintResult> PrintAsync(
        PrintPayload payload,
        CancellationToken cancellationToken = default);

    Task<PrinterStatus> GetStatusAsync();
}