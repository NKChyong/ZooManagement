using ZooManagement.Domain.Entities;
using ZooManagement.Domain.ValueObjects;
using ZooManagement.Domain.Enums;
using Xunit;

public class AnimalTests
{
    [Fact]
    public void MoveTo_RaisesEvent()
    {
        // arrange
        var animal = new Animal(Guid.NewGuid(), new Species("Lion"),
                                new AnimalName("Simba"),
                                DateTime.UtcNow.AddYears(-2), Gender.Male,
                                new Food("Meat"));
        var newEnclosure = Guid.NewGuid();

        // act
        animal.MoveTo(newEnclosure);

        // assert
        Assert.Contains(animal.DomainEvents,
            e => e.GetType().Name == "AnimalMovedEvent");
    }
}
