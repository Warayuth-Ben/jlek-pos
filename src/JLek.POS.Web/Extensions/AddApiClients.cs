using JLek.POS.Web.Clients.Kitchen;
using JLek.POS.Web.Clients.Menu;
using JLek.POS.Web.Clients.Orders;
using JLek.POS.Web.Clients.Payments;
using JLek.POS.Web.Clients.Receipts;
using JLek.POS.Web.Clients.Reports;
using JLek.POS.Web.Clients.Tables;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApiClientExtensions
{
    public static IServiceCollection AddApiClients(
        this IServiceCollection services,
        string baseAddress)
    {
        services.AddScoped(sp =>
        {
            var client = new HttpClient { BaseAddress = new Uri(baseAddress) };
            return client;
        });

        services.AddScoped<ITableClient, TableClient>();
        services.AddScoped<IOrderClient, OrderClient>();
        services.AddScoped<IMenuClient, MenuClient>();
        services.AddScoped<IPaymentClient, PaymentClient>();
        services.AddScoped<IReceiptClient, ReceiptClient>();
        services.AddScoped<IKitchenClient, KitchenClient>();
        services.AddScoped<IReportClient, ReportClient>();

        return services;
    }
}