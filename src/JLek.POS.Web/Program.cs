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

        await builder.Build().RunAsync();
    }
}