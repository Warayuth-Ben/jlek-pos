using JLek.POS.Application.Features.Receipt.DTOs;
using JLek.POS.Application.Features.Receipt.Models;

namespace JLek.POS.Application.Abstractions;

public interface IReceiptFormatter
{
    ReceiptDocument FormatCustomerReceipt(
        CustomerReceiptData data, bool isReprint = false);

    ReceiptDocument FormatKitchenTicket(
        KitchenTicketReceiptData data);

    ReceiptDocument FormatRefundReceipt(
        RefundReceiptData data, bool isReprint = false);
}