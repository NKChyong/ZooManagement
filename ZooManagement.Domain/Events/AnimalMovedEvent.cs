namespace ZooManagement.Domain.Events;
public sealed record AnimalMovedEvent(Guid AnimalId,
    Guid FromEnclosureId,
    Guid ToEnclosureId,
    DateTime OccurredOn)
    : IDomainEvent;