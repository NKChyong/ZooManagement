using Microsoft.Extensions.DependencyInjection;
using ZooManagement.Application.Interfaces;
using ZooManagement.Infrastructure.Persistence;
using ZooManagement.Infrastructure.Repositories;
using ZooManagement.Infrastructure.Messaging;

namespace ZooManagement.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<InMemoryDbContext>();

        services.AddScoped<IAnimalRepository, InMemoryAnimalRepository>();
        services.AddScoped<IEnclosureRepository, InMemoryEnclosureRepository>();
        services.AddScoped<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();

        services.AddSingleton<IEventPublisher, SimpleEventPublisher>();
        return services;
    }
}