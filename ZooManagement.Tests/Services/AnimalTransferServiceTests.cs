using Xunit;
using ZooManagement.Infrastructure.Persistence;
using ZooManagement.Infrastructure.Repositories;
using ZooManagement.Application.Services;
using ZooManagement.Domain.Entities;
using ZooManagement.Domain.ValueObjects;
using ZooManagement.Domain.Enums;

namespace ZooManagement.Tests.Services;

public class AnimalTransferServiceTests
{
    private readonly InMemoryDbContext _db = new();

    [Fact]
    public async Task MoveAsync_AnimalChangesEnclosure()
    {
        // arrange
        var animal   = new Animal(Guid.NewGuid(), new Species("Lion"), new AnimalName("Alex"),
                                  DateTime.UtcNow.AddYears(-4), Gender.Male, new Food("Meat"));
        var encA     = new Enclosure(Guid.NewGuid(), new EnclosureType("Predators"), 200, 5);
        var encB     = new Enclosure(Guid.NewGuid(), new EnclosureType("Predators"), 200, 5);

        encA.AddAnimal(animal);

        _db.Animals.Add(animal);
        _db.Enclosures.AddRange([encA, encB]);

        var animalRepo    = new InMemoryAnimalRepository(_db);
        var enclosureRepo = new InMemoryEnclosureRepository(_db);

        var service = new AnimalTransferService(animalRepo, enclosureRepo);

        // act
        await service.MoveAsync(animal.Id, encB.Id);

        // assert
        Assert.Contains(animal.Id, encB.AnimalIds);
        Assert.DoesNotContain(animal.Id, encA.AnimalIds);
        Assert.Equal(encB.Id, animal.EnclosureId);
    }
}
