using JLek.POS.Application.Abstractions;
using JLek.POS.Application.Features.Receipt.Configuration;
using JLek.POS.Application.Features.Receipt.DTOs;
using JLek.POS.Application.Features.Receipt.Models;

namespace JLek.POS.Application.Features.Receipt.Services;

public sealed class ReceiptFormatter : IReceiptFormatter
{
    private readonly ReceiptConfiguration _config;

    public ReceiptFormatter(ReceiptConfiguration config)
    {
        _config = config;
    }

    public ReceiptDocument FormatCustomerReceipt(
        CustomerReceiptData data, bool isReprint = false)
    {
        var lines = new List<ReceiptLine>();

        if (isReprint)
        {
            lines.Add(new ReceiptLine { Text = "*** REPRINT ***", Alignment = ReceiptAlignment.Center, Bold = true, DoubleWidth = true });
        }

        lines.Add(new ReceiptLine { Text = _config.ShopName, Alignment = ReceiptAlignment.Center, Bold = true, DoubleWidth = true });
        lines.Add(new ReceiptLine { Text = _config.ShopAddress, Alignment = ReceiptAlignment.Center });
        lines.Add(new ReceiptLine { Text = $"Tel: {_config.ShopPhone}", Alignment = ReceiptAlignment.Center });
        if (!string.IsNullOrEmpty(_config.TaxId))
            lines.Add(new ReceiptLine { Text = $"Tax ID: {_config.TaxId}", Alignment = ReceiptAlignment.Center });

        lines.Add(new ReceiptLine { Text = new string('-', _config.PaperWidth), Alignment = ReceiptAlignment.Center });
        lines.Add(new ReceiptLine { Text = $"{data.ReceiptNumber}  {data.PrintedAt:dd/MM/yyyy HH:mm}", Alignment = ReceiptAlignment.Left });

        if (!string.IsNullOrEmpty(data.TableName))
        {
            lines.Add(new ReceiptLine { Text = $"โต๊ะ: {data.TableName}", Alignment = ReceiptAlignment.Left });
        }

        lines.Add(new ReceiptLine { Text = new string('-', _config.PaperWidth), Alignment = ReceiptAlignment.Center });

        foreach (var item in data.Items)
        {
            lines.Add(new ReceiptLine { Text = item.Name, Alignment = ReceiptAlignment.Left, Bold = true });
            lines.Add(new ReceiptLine { Text = $"  {item.Quantity,3}x @ {item.UnitPrice,8:F2} = {item.Total,10:F2}", Alignment = ReceiptAlignment.Left });
        }

        lines.Add(new ReceiptLine { Text = new string('-', _config.PaperWidth), Alignment = ReceiptAlignment.Center });
        lines.Add(new ReceiptLine { Text = $"รวมทั้งสิ้น         {data.Total,14:F2}", Alignment = ReceiptAlignment.Right, Bold = true });
        lines.Add(new ReceiptLine { Text = $"รับเงิน             {data.AmountReceived,14:F2}", Alignment = ReceiptAlignment.Right });
        lines.Add(new ReceiptLine { Text = $"เงินทอน            {data.Change,14:F2}", Alignment = ReceiptAlignment.Right });
        lines.Add(new ReceiptLine { Text = $"วิธีชำระ: {data.PaymentMethod}", Alignment = ReceiptAlignment.Left });

        lines.Add(new ReceiptLine { Text = new string('-', _config.PaperWidth), Alignment = ReceiptAlignment.Center });
        lines.Add(new ReceiptLine { Text = _config.Footer, Alignment = ReceiptAlignment.Center, Bold = true });

        return new ReceiptDocument
        {
            Title = "ใบเสร็จรับเงิน",
            ReceiptNumber = data.ReceiptNumber,
            ReprintLabel = isReprint ? "*** REPRINT ***" : null,
            Lines = lines
        };
    }

    public ReceiptDocument FormatKitchenTicket(KitchenTicketReceiptData data)
    {
        var lines = new List<ReceiptLine>();

        lines.Add(new ReceiptLine { Text = "*** KITCHEN TICKET ***", Alignment = ReceiptAlignment.Center, Bold = true, DoubleWidth = true });
        lines.Add(new ReceiptLine { Text = $"Ticket #{data.TicketNumber}", Alignment = ReceiptAlignment.Center, Bold = true });
        lines.Add(new ReceiptLine { Text = $"{data.PrintedAt:HH:mm}", Alignment = ReceiptAlignment.Left });
        lines.Add(new ReceiptLine { Text = new string('-', _config.PaperWidth), Alignment = ReceiptAlignment.Center });

        foreach (var item in data.Items)
        {
            lines.Add(new ReceiptLine { Text = $"  {item.Quantity}x {item.Name}", Alignment = ReceiptAlignment.Left, Bold = true });
            if (!string.IsNullOrEmpty(item.Notes))
            {
                lines.Add(new ReceiptLine { Text = $"    (Note: {item.Notes})", Alignment = ReceiptAlignment.Left });
            }
        }

        lines.Add(new ReceiptLine { Text = new string('-', _config.PaperWidth), Alignment = ReceiptAlignment.Center });
        lines.Add(new ReceiptLine { Text = _config.Footer, Alignment = ReceiptAlignment.Center });

        return new ReceiptDocument
        {
            Title = "Kitchen Ticket",
            Lines = lines
        };
    }

    public ReceiptDocument FormatRefundReceipt(RefundReceiptData data, bool isReprint = false)
    {
        var lines = new List<ReceiptLine>();

        if (isReprint)
        {
            lines.Add(new ReceiptLine { Text = "*** REPRINT ***", Alignment = ReceiptAlignment.Center, Bold = true, DoubleWidth = true });
        }

        lines.Add(new ReceiptLine { Text = "*** REFUND RECEIPT ***", Alignment = ReceiptAlignment.Center, Bold = true, DoubleWidth = true });
        lines.Add(new ReceiptLine { Text = _config.ShopName, Alignment = ReceiptAlignment.Center });
        lines.Add(new ReceiptLine { Text = $"{data.ReceiptNumber}", Alignment = ReceiptAlignment.Center });
        lines.Add(new ReceiptLine { Text = $"{data.PrintedAt:dd/MM/yyyy HH:mm}", Alignment = ReceiptAlignment.Left });
        lines.Add(new ReceiptLine { Text = new string('-', _config.PaperWidth), Alignment = ReceiptAlignment.Center });
        lines.Add(new ReceiptLine { Text = $"Original Receipt: {data.OriginalReceiptNumber}", Alignment = ReceiptAlignment.Left });
        lines.Add(new ReceiptLine { Text = $"Refund Amount:   {data.AmountRefunded,14:F2}", Alignment = ReceiptAlignment.Right, Bold = true });
        lines.Add(new ReceiptLine { Text = $"Payment Method:  {data.PaymentMethod}", Alignment = ReceiptAlignment.Left });
        if (!string.IsNullOrEmpty(data.Reason))
        {
            lines.Add(new ReceiptLine { Text = $"Reason: {data.Reason}", Alignment = ReceiptAlignment.Left });
        }
        lines.Add(new ReceiptLine { Text = new string('-', _config.PaperWidth), Alignment = ReceiptAlignment.Center });
        lines.Add(new ReceiptLine { Text = _config.Footer, Alignment = ReceiptAlignment.Center });

        return new ReceiptDocument
        {
            Title = "Refund Receipt",
            ReceiptNumber = data.ReceiptNumber,
            ReprintLabel = isReprint ? "*** REPRINT ***" : null,
            Lines = lines
        };
    }
}