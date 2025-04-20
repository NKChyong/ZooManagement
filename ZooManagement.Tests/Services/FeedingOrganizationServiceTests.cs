using Xunit;
using ZooManagement.Infrastructure.Persistence;
using ZooManagement.Infrastructure.Repositories;
using ZooManagement.Application.Services;
using ZooManagement.Domain.Entities;
using ZooManagement.Domain.ValueObjects;
using ZooManagement.Domain.Enums;

namespace ZooManagement.Tests.Services;

public class FeedingOrganizationServiceTests
{
    private readonly InMemoryDbContext _db = new();

    [Fact]
    public async Task AddAndExecuteFeeding_WorksCorrectly()
    {
        // arrange
        var animal = new Animal(Guid.NewGuid(), new Species("Seal"),
                                new AnimalName("Flippy"), DateTime.UtcNow.AddYears(-2),
                                Gender.Female, new Food("Fish"));
        _db.Animals.Add(animal);

        var animalRepo   = new InMemoryAnimalRepository(_db);
        var schedRepo    = new InMemoryFeedingScheduleRepository(_db);

        var service = new FeedingOrganizationService(schedRepo, animalRepo);

        // act
        await service.AddFeedingAsync(animal.Id, DateTime.UtcNow.AddMinutes(30), new Food("Fish"));
        var schedule = _db.FeedingSchedules.Single();

        await service.ExecuteFeedingAsync(schedule.Id);

        // assert
        Assert.True(schedule.Executed);
    }
}
