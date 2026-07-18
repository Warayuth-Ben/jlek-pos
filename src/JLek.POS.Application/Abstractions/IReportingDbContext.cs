using JLek.POS.Domain.Kitchen;
using JLek.POS.Domain.Orders;
using JLek.POS.Domain.Payments;
using Microsoft.EntityFrameworkCore;

namespace JLek.POS.Application.Abstractions;

/// <summary>
/// Read-only DbContext interface for reporting purposes.
/// Only exposes the DbSets needed for reading report data.
/// This prevents reporting from modifying any data.
/// </summary>
public interface IReportingDbContext
{
    DbSet<Order> Orders { get; }
    DbSet<Payment> Payments { get; }
    DbSet<KitchenTicket> KitchenTickets { get; }
}