using JLek.POS.Application.Abstractions;
using JLek.POS.Application.Features.Receipt.DTOs;
using JLek.POS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JLek.POS.Infrastructure.Printing;

public sealed class ReceiptDataProvider : IReceiptDataProvider
{
    private readonly ApplicationDbContext _context;

    public ReceiptDataProvider(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerReceiptData?> GetCustomerReceiptDataAsync(
        Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders
            .AsNoTracking()
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id.Value == orderId, cancellationToken);

        if (order is null)
            return null;

        var payment = await _context.Payments
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.OrderId.Value == orderId
                                   && p.Status == Domain.Payments.PaymentStatus.Completed,
                cancellationToken);

        return new CustomerReceiptData
        {
            ReceiptNumber = $"RC{DateTime.UtcNow:yyyyMMddHHmmss}",
            PrintedAt = DateTime.UtcNow,
            Items = order.Items.Select(i => new ReceiptItemData
            {
                Name = $"Item {i.MenuItemId.ToString()[..8]}",
                Quantity = i.Quantity.Value,
                UnitPrice = i.UnitPrice.Amount,
                Total = i.TotalPrice.Amount
            }).ToList(),
            Total = order.Total.Amount,
            AmountReceived = payment?.AmountReceived.Amount ?? order.Total.Amount,
            Change = payment?.Change.Amount ?? 0m,
            PaymentMethod = payment?.Method.ToString() ?? "Unknown"
        };
    }

    public async Task<KitchenTicketReceiptData?> GetKitchenReceiptDataAsync(
        int ticketNumber, CancellationToken cancellationToken = default)
    {
        var ticket = await _context.KitchenTickets
            .AsNoTracking()
            .Include(t => t.Items)
            .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber, cancellationToken);

        if (ticket is null)
            return null;

        return new KitchenTicketReceiptData
        {
            TicketNumber = ticket.TicketNumber,
            PrintedAt = DateTime.UtcNow,
            Items = ticket.Items.Select(i => new KitchenReceiptItemData
            {
                Name = i.ItemName,
                Quantity = i.Quantity,
                Notes = i.Notes
            }).ToList()
        };
    }

    public async Task<RefundReceiptData?> GetRefundReceiptDataAsync(
        Guid paymentId, CancellationToken cancellationToken = default)
    {
        var payment = await _context.Payments
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id.Value == paymentId, cancellationToken);

        if (payment is null)
            return null;

        return new RefundReceiptData
        {
            ReceiptNumber = $"RF{DateTime.UtcNow:yyyyMMddHHmmss}",
            OriginalReceiptNumber = $"RC{DateTime.UtcNow:yyyyMMddHHmmss}",
            PrintedAt = DateTime.UtcNow,
            AmountRefunded = payment.AmountReceived.Amount,
            Reason = payment.RefundReason,
            PaymentMethod = payment.Method.ToString()
        };
    }
}