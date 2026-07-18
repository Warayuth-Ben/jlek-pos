using JLek.POS.Application.Features.Receipt.Models;
using JLek.POS.Printing.Models;

namespace JLek.POS.Printing.Abstractions;

public interface IRenderer
{
    Task<PrintPayload> RenderAsync(
        ReceiptDocument document,
        CancellationToken cancellationToken = default);
}