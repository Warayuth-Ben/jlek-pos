using JLek.POS.Application.Features.Receipt.Models;
using JLek.POS.Printing.Abstractions;
using JLek.POS.Printing.Models;

namespace JLek.POS.Printing.Renderer;

public sealed class EscPosRenderer : IRenderer
{
    private readonly RenderOptions _options;

    public EscPosRenderer(RenderOptions options)
    {
        _options = options;
    }

    public Task<PrintPayload> RenderAsync(
        ReceiptDocument document,
        CancellationToken cancellationToken = default)
    {
        var buffer = new List<byte>();

        // 1. Initialize printer
        buffer.AddRange(EscPosCommands.Initialize.ToArray());

        // 2. Set code page (encoding)
        buffer.AddRange(EscPosCodePages.GetCodePageCommand(_options.Encoding));

        // 3. Title
        if (!string.IsNullOrEmpty(document.Title))
        {
            AppendLine(buffer, document.Title, ReceiptAlignment.Center, bold: true, doubleWidth: true, doubleHeight: false);
        }

        // 4. Reprint label
        if (!string.IsNullOrEmpty(document.ReprintLabel))
        {
            AppendLine(buffer, document.ReprintLabel, ReceiptAlignment.Center, bold: true, doubleWidth: true, doubleHeight: false);
        }

        // 5. Receipt number
        if (!string.IsNullOrEmpty(document.ReceiptNumber))
        {
            AppendLine(buffer, $"No. {document.ReceiptNumber}", ReceiptAlignment.Left, bold: false, doubleWidth: false, doubleHeight: false);
        }

        // 6. Lines
        foreach (var line in document.Lines)
        {
            AppendLine(buffer, line.Text, line.Alignment, line.Bold, line.DoubleWidth, line.DoubleHeight);
        }

        // 7. Paper cut
        if (_options.CutPaper)
        {
            buffer.AddRange(EscPosCommands.CutPaper(EscPosCutMode.PartialCut));
        }

        var payload = new PrintPayload
        {
            Data = buffer.ToArray(),
            MimeType = EscPosConstants.EscPosMimeType,
            Format = EscPosConstants.EscPosFormat,
            Description = $"ESC/POS receipt: {document.Title}"
        };

        return Task.FromResult(payload);
    }

    private void AppendLine(
        List<byte> buffer,
        string text,
        ReceiptAlignment alignment,
        bool bold,
        bool doubleWidth,
        bool doubleHeight)
    {
        // Set alignment
        buffer.AddRange(EscPosCommands.SetAlignment(MapAlignment(alignment)));

        // Reset character size first
        buffer.AddRange(EscPosCommands.ResetCharacterSize.ToArray());

        // Set character size
        if (doubleWidth || doubleHeight)
        {
            buffer.AddRange(EscPosCommands.SetCharacterSize(doubleWidth, doubleHeight));
        }

        // Set bold
        buffer.AddRange(EscPosCommands.SetBold(bold));

        // Encode text
        var textBytes = _options.Encoding.GetBytes(text);
        buffer.AddRange(textBytes);

        // Line feed
        buffer.AddRange(EscPosCommands.LineFeed.ToArray());
    }

    private static EscPosAlignment MapAlignment(ReceiptAlignment alignment)
    {
        return alignment switch
        {
            ReceiptAlignment.Center => EscPosAlignment.Center,
            ReceiptAlignment.Right => EscPosAlignment.Right,
            _ => EscPosAlignment.Left
        };
    }
}