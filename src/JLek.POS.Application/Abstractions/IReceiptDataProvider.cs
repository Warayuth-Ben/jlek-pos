using JLek.POS.Application.Features.Receipt.DTOs;

namespace JLek.POS.Application.Abstractions;

public interface IReceiptDataProvider
{
    Task<CustomerReceiptData?> GetCustomerReceiptDataAsync(
        Guid orderId, CancellationToken cancellationToken = default);

    Task<KitchenTicketReceiptData?> GetKitchenReceiptDataAsync(
        int ticketNumber, CancellationToken cancellationToken = default);

    Task<RefundReceiptData?> GetRefundReceiptDataAsync(
        Guid paymentId, CancellationToken cancellationToken = default);
}