using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace JLek.POS.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        // API base URL — configured in wwwroot/appsettings.json
        var apiBaseUrl = builder.Configuration["ApiBaseUrl"]
            ?? builder.HostEnvironment.BaseAddress;

        builder.Services.AddApiClients(apiBaseUrl);

        // Presentation services
        builder.Services.AddSingleton<JLek.POS.Web.Presentation.Context.ContextStore>();
        builder.Services.AddSingleton<JLek.POS.Web.Presentation.Context.WorkspaceContext>();
        builder.Services.AddTransient<JLek.POS.Web.Presentation.Cashier.CashierStore>();
        builder.Services.AddScoped<JLek.POS.Web.Presentation.Abstractions.ICashierDataProvider, JLek.POS.Web.Presentation.Data.ApiCashierDataProvider>();
        builder.Services.AddScoped<JLek.POS.Web.Presentation.Data.ApiOrderProvider>();

        await builder.Build().RunAsync();
    }
}