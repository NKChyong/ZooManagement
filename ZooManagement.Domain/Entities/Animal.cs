using ZooManagement.Domain.ValueObjects;
using ZooManagement.Domain.Enums;
using ZooManagement.Domain.Events;

namespace ZooManagement.Domain.Entities;

public class Animal
{
    public Guid Id { get; }
    public Species Species { get; private set; }
    public AnimalName Name { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public Gender Gender { get; private set; }
    public Food FavouriteFood { get; private set; }
    public HealthStatus Status { get; private set; }
    public Guid? EnclosureId { get; private set; }

    private readonly List<IDomainEvent> _events = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _events.AsReadOnly();

    public Animal(Guid id, Species species, AnimalName name, DateTime dob,
        Gender gender, Food favouriteFood, HealthStatus status = HealthStatus.Healthy)
    {
        Id = id;
        Species = species;
        Name = name;
        DateOfBirth = dob;
        Gender = gender;
        FavouriteFood = favouriteFood;
        Status = status;
    }

    public void Feed(Food food, DateTime time)
    {
        if (food != FavouriteFood) throw new InvalidOperationException("Неверный корм.");
        _events.Add(new FeedingTimeEvent(Id, time, DateTime.UtcNow));
    }

    public void Heal() => Status = HealthStatus.Healthy;

    public void MoveTo(Guid toEnclosureId)
    {
        if (EnclosureId == toEnclosureId) return;
        var from = EnclosureId;
        EnclosureId = toEnclosureId;
        _events.Add(new AnimalMovedEvent(Id, from ?? Guid.Empty, toEnclosureId, DateTime.UtcNow));
    }

    internal void ClearEvents() => _events.Clear();
}