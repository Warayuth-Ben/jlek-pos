using JLek.POS.Application.Abstractions;
using JLek.POS.Application.Abstractions.EventHandling;
using JLek.POS.Domain.Catalog;
using JLek.POS.Domain.Common.Primitives;
using JLek.POS.Domain.Kitchen;
using JLek.POS.Domain.Orders;
using JLek.POS.Domain.Payments;
using JLek.POS.Domain.Tables;
using Microsoft.EntityFrameworkCore;

namespace JLek.POS.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext, IReportingDbContext
{
    private readonly IDomainEventDispatcher _dispatcher;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDomainEventDispatcher dispatcher)
        : base(options)
    {
        _dispatcher = dispatcher;
    }

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();

    public DbSet<Ingredient> Ingredients => Set<Ingredient>();

    public DbSet<DiningTable> DiningTables => Set<DiningTable>();

    public DbSet<KitchenTicket> KitchenTickets => Set<KitchenTicket>();

    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var domainEvents = GetDomainEvents();

        var result = await base.SaveChangesAsync(cancellationToken);

        if (domainEvents.Count > 0)
        {
            await _dispatcher.DispatchAsync(domainEvents, cancellationToken);

            ClearDomainEvents();
        }

        return result;
    }

    private List<IDomainEvent> GetDomainEvents()
    {
        var events = new List<IDomainEvent>();

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is AggregateRoot<object> typed)
            {
                if (typed.DomainEvents.Count > 0)
                {
                    events.AddRange(typed.DomainEvents);
                }
            }
        }

        return events;
    }

    private void ClearDomainEvents()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is AggregateRoot<object> typed)
            {
                if (typed.DomainEvents.Count > 0)
                {
                    typed.ClearDomainEvents();
                }
            }
        }
    }
}
