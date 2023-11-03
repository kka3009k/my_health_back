using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
using Microsoft.Extensions.DependencyInjection;
using MyHealth.Cabinet.MiddleWares;
using MyHealth.Cabinet.Services;
using System.Text;

namespace MyHealth.Cabinet.Extension;

/// <summary>
/// Настройка приложения
/// </summary>
public static class ServiceExtension
{
    public static void ConfigureApp(this IServiceCollection services, IConfiguration pConfiguration)
    {
        services.AddScoped<UserStateService>();
        services.AddBlazoredLocalStorage();
        services.AddScoped(sp => new HttpClient(
            new CustomHttpHadler(services.BuildServiceProvider().GetRequiredService<UserStateService>()))
        { BaseAddress = new Uri(pConfiguration["BackendUrl"]) });
        services.AddBlazoredToast();
        services.AddBlazoredModal();
        services.AddScoped<SpinnerService>();
        services.AddScoped<NavigationService>();

    }

    public static void ConfigureDbContext(this IServiceCollection services)
    {

    }

    public static void ConfigureJwtAuthentication(this IServiceCollection services)
    {

    }
}