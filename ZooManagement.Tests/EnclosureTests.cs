using Xunit;
using ZooManagement.Domain.Entities;
using ZooManagement.Domain.ValueObjects;

namespace ZooManagement.Tests;

public class EnclosureTests
{
    [Fact]
    public void AddAnimal_IncreasesAnimalCount()
    {
        var enclosure = new Enclosure(Guid.NewGuid(), new EnclosureType("Birds"), 50, 2);
        var animal = new ZooManagement.Domain.Entities.Animal(Guid.NewGuid(),
                        new Species("Parrot"), new AnimalName("Coco"),
                        DateTime.UtcNow.AddYears(-1), ZooManagement.Domain.Enums.Gender.Unknown,
                        new Food("Seeds"));

        enclosure.AddAnimal(animal);

        Assert.Single(enclosure.AnimalIds);
        Assert.Equal(enclosure.Id, animal.EnclosureId);
    }

    [Fact]
    public void AddAnimal_WhenFull_Throws()
    {
        var enclosure = new Enclosure(Guid.NewGuid(), new EnclosureType("Mini"), 10, 1);
        var a1 = new ZooManagement.Domain.Entities.Animal(Guid.NewGuid(), new Species("Mouse"),
                 new AnimalName("M1"), DateTime.UtcNow, ZooManagement.Domain.Enums.Gender.Unknown, new Food("Grain"));
        var a2 = new ZooManagement.Domain.Entities.Animal(Guid.NewGuid(), new Species("Mouse"),
                 new AnimalName("M2"), DateTime.UtcNow, ZooManagement.Domain.Enums.Gender.Unknown, new Food("Grain"));

        enclosure.AddAnimal(a1);

        Assert.Throws<InvalidOperationException>(() => enclosure.AddAnimal(a2));
    }
}
