using JLek.POS.Application.Abstractions;
using JLek.POS.Application.Abstractions.EventHandling;
using JLek.POS.Application.Abstractions.Repositories;
using JLek.POS.Application.Features.Receipt.Configuration;
using JLek.POS.Domain.Common.Abstractions;
using JLek.POS.Infrastructure.EventHandling;
using JLek.POS.Infrastructure.Persistence;
using JLek.POS.Infrastructure.Printing;
using JLek.POS.Infrastructure.Repositories;
using JLek.POS.Infrastructure.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JLek.POS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddScoped<IIngredientRepository, IngredientRepository>();
        services.AddScoped<IDiningTableRepository, DiningTableRepository>();
        services.AddScoped<IKitchenTicketRepository, KitchenTicketRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        // Reporting
        services.AddScoped<IClock, SystemClock>();
        services.AddScoped<IReportingDbContext>(
            sp => sp.GetRequiredService<ApplicationDbContext>());

        // Receipt
        services.AddScoped<IReceiptDataProvider, ReceiptDataProvider>();
        services.AddScoped<IReceiptPrinter, NullReceiptPrinter>();
        services.AddScoped<IKitchenPrinter, NullKitchenPrinter>();

        return services;
    }
}
