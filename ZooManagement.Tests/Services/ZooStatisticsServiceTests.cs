using Xunit;
using ZooManagement.Infrastructure.Persistence;
using ZooManagement.Infrastructure.Repositories;
using ZooManagement.Application.Services;
using ZooManagement.Domain.Entities;
using ZooManagement.Domain.ValueObjects;
using ZooManagement.Domain.Enums;

namespace ZooManagement.Tests.Services;

public class ZooStatisticsServiceTests
{
    private readonly InMemoryDbContext _db = new();

    [Fact]
    public async Task GetAsync_ReturnsCorrectNumbers()
    {
        // arrange
        var enc1 = new Enclosure(Guid.NewGuid(), new EnclosureType("Aviary"), 100, 3);
        var enc2 = new Enclosure(Guid.NewGuid(), new EnclosureType("Aquarium"), 150, 2);

        var animal1 = new Animal(Guid.NewGuid(), new Species("Eagle"),  new AnimalName("Sky"),
                                 DateTime.UtcNow.AddYears(-6), Gender.Male,   new Food("Meat"));
        var animal2 = new Animal(Guid.NewGuid(), new Species("Shark"), new AnimalName("Jaws"),
                                 DateTime.UtcNow.AddYears(-8), Gender.Female, new Food("Fish"));

        enc1.AddAnimal(animal1);
        enc2.AddAnimal(animal2);

        _db.Enclosures.AddRange([enc1, enc2]);
        _db.Animals.AddRange([animal1, animal2]);

        var animalRepo    = new InMemoryAnimalRepository(_db);
        var enclosureRepo = new InMemoryEnclosureRepository(_db);

        var statsService = new ZooStatisticsService(animalRepo, enclosureRepo);

        // act
        var (animalCount, enclosureCount, freeEnclosures) = await statsService.GetAsync();

        // assert
        Assert.Equal(2, animalCount);
        Assert.Equal(2, enclosureCount);
        Assert.Equal(0, freeEnclosures);
    }
}
