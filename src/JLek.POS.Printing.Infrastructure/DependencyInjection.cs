using JLek.POS.Printing.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace JLek.POS.Printing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPrintingInfrastructure(
        this IServiceCollection services)
    {
        // Register placeholder for IRenderer (concrete implementation deferred)
        // services.AddScoped<IRenderer, EscPosRenderer>();

        // Register NullPrinterAdapter as default
        services.AddScoped<IPrinterAdapter, NullPrinterAdapter>();

        return services;
    }
}