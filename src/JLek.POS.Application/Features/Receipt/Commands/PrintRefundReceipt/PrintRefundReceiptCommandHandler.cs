using JLek.POS.Application.Abstractions;
using JLek.POS.Application.Features.Receipt.Models;
using JLek.POS.Domain.Common.Abstractions;

namespace JLek.POS.Application.Features.Receipt.Commands.PrintRefundReceipt;

public sealed class PrintRefundReceiptCommandHandler
{
    private readonly IReceiptDataProvider _dataProvider;
    private readonly IReceiptFormatter _formatter;
    private readonly IReceiptPrinter _printer;
    private readonly IClock _clock;

    public PrintRefundReceiptCommandHandler(
        IReceiptDataProvider dataProvider,
        IReceiptFormatter formatter,
        IReceiptPrinter printer,
        IClock clock)
    {
        _dataProvider = dataProvider;
        _formatter = formatter;
        _printer = printer;
        _clock = clock;
    }

    public async Task<PrintResult> Handle(
        PrintRefundReceiptCommand command,
        CancellationToken cancellationToken = default)
    {
        var startedAt = _clock.UtcNow;

        var data = await _dataProvider.GetRefundReceiptDataAsync(
            command.PaymentId, cancellationToken);

        if (data is null)
        {
            return new PrintResult
            {
                Success = false,
                ErrorMessage = "Payment not found",
                StartedAt = startedAt,
                FinishedAt = _clock.UtcNow,
                Status = "Failed",
                PrinterName = _printer.PrinterName,
                Copies = command.Copies
            };
        }

        var document = _formatter.FormatRefundReceipt(data, command.IsReprint);
        var printResult = await _printer.PrintAsync(document, cancellationToken);

        return new PrintResult
        {
            Success = printResult.Success,
            ErrorMessage = printResult.ErrorMessage,
            StartedAt = startedAt,
            FinishedAt = _clock.UtcNow,
            Status = printResult.Success ? "Completed" : "Failed",
            PrinterName = _printer.PrinterName,
            Copies = command.Copies
        };
    }
}