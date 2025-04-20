// FeedingOrganizationService.cs
using ZooManagement.Application.Interfaces;
using ZooManagement.Domain.Entities;
using ZooManagement.Domain.ValueObjects;

namespace ZooManagement.Application.Services;

public sealed class FeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _schedules;
    private readonly IAnimalRepository _animals;

    public FeedingOrganizationService(IFeedingScheduleRepository schedules,
        IAnimalRepository animals)
    {
        _schedules = schedules;
        _animals   = animals;
    }

    public async Task AddFeedingAsync(Guid animalId, DateTime time, Food food)
    {
        var animal = await _animals.GetAsync(animalId)
                     ?? throw new KeyNotFoundException("Животное не найдено");

        var schedule = new FeedingSchedule(Guid.NewGuid(), animalId, time, food);
        await _schedules.AddAsync(schedule);
        await _schedules.SaveChangesAsync();
    }

    public async Task ExecuteFeedingAsync(Guid scheduleId)
    {
        var schedule = await _schedules.GetAsync(scheduleId)
                       ?? throw new KeyNotFoundException("Нет такого расписания");

        schedule.MarkExecuted(DateTime.UtcNow);
        await _schedules.SaveChangesAsync();
    }
}