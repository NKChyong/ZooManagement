// ZooStatisticsService.cs
using ZooManagement.Application.Interfaces;

namespace ZooManagement.Application.Services;

public sealed class ZooStatisticsService
{
    private readonly IAnimalRepository _animals;
    private readonly IEnclosureRepository _enclosures;

    public ZooStatisticsService(IAnimalRepository animals,
        IEnclosureRepository enclosures)
    {
        _animals = animals;
        _enclosures = enclosures;
    }

    public async Task<(int totalAnimals, int totalEnclosures,
        int freeEnclosures)> GetAsync()
    {
        var allAnimals = await _animals.GetAllAsync();
        var allEnclosures = await _enclosures.GetAllAsync();
        var free = allEnclosures.Count(e => e.AnimalIds.Count == 0);

        return (allAnimals.Count, allEnclosures.Count, free);
    }
}