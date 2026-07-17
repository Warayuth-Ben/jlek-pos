using JLek.POS.Domain.Catalog;
using JLek.POS.Domain.Kitchen;
using JLek.POS.Domain.Orders;
using JLek.POS.Domain.Payments;
using JLek.POS.Domain.Tables;
using Microsoft.EntityFrameworkCore;

namespace JLek.POS.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
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
}