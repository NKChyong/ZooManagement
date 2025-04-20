// FeedingSchedule.cs
using ZooManagement.Domain.ValueObjects;
using ZooManagement.Domain.Events;

namespace ZooManagement.Domain.Entities;

public class FeedingSchedule
{
    public Guid Id { get; }
    public Guid AnimalId { get; private set; }
    public DateTime FeedingTime { get; private set; }
    public Food Food { get; private set; }
    public bool Executed { get; private set; }

    private readonly List<IDomainEvent> _events = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _events.AsReadOnly();

    public FeedingSchedule(Guid id, Guid animalId, DateTime feedingTime, Food food)
    {
        Id = id;
        AnimalId = animalId;
        FeedingTime = feedingTime;
        Food = food;
    }

    public void Reschedule(DateTime newTime) => FeedingTime = newTime;

    public void MarkExecuted(DateTime when)
    {
        Executed = true;
        _events.Add(new FeedingTimeEvent(AnimalId, when, DateTime.UtcNow));
    }

    internal void ClearEvents() => _events.Clear();
}