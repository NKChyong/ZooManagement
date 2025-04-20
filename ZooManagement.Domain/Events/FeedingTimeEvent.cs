namespace ZooManagement.Domain.Events;
public sealed record FeedingTimeEvent(Guid AnimalId,
    DateTime FeedingTime,
    DateTime OccurredOn)
    : IDomainEvent;