using Microsoft.Extensions.DependencyInjection;
using ZooManagement.Application.Services;

namespace ZooManagement.Application;

/// <summary>
/// Расширения для регистрации прикладных сервисов в DI‑контейнере.
/// Подключайте в Program.cs: services.AddApplication();
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // регистрируем внутренние бизнес‑сервисы слоя Application
        services.AddScoped<AnimalTransferService>();
        services.AddScoped<FeedingOrganizationService>();
        services.AddScoped<ZooStatisticsService>();

        return services;
    }
}