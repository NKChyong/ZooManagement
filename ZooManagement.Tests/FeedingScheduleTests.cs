using Xunit;
using ZooManagement.Domain.Entities;
using ZooManagement.Domain.ValueObjects;

namespace ZooManagement.Tests;

public class FeedingScheduleTests
{
    [Fact]
    public void MarkExecuted_SetsFlagAndRaisesEvent()
    {
        var schedule = new FeedingSchedule(Guid.NewGuid(),
                                           Guid.NewGuid(),
                                           DateTime.UtcNow.AddHours(1),
                                           new Food("Fish"));

        schedule.MarkExecuted(DateTime.UtcNow);

        Assert.True(schedule.Executed);
        Assert.Single(schedule.DomainEvents
                     .Where(e => e.GetType().Name == nameof(ZooManagement.Domain.Events.FeedingTimeEvent)));
    }
}
