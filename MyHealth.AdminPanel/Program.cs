using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyHealth.AdminPanel.Client;
using MyHealth.AdminPanel.Handlers;
using MyHealth.AdminPanel.Services;

namespace MyHealth.AdminPanel.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<UserStateService>();
            builder.Services.AddScoped(sp =>
                new HttpClient(new CustomHttpHandler(builder.Services.BuildServiceProvider().GetRequiredService<UserStateService>()))
                {
                    BaseAddress = new Uri(builder.Configuration["BackendUrl"]),
                });

            await builder.Build().RunAsync();
        }
    }
}