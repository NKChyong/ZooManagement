// Enclosure.cs
using ZooManagement.Domain.ValueObjects;
using ZooManagement.Domain.Enums;

namespace ZooManagement.Domain.Entities;

public class Enclosure
{
    public Guid Id { get; }
    public EnclosureType Type { get; private set; }
    public double SizeSqM { get; private set; }
    public int MaxCapacity { get; private set; }

    private readonly List<Guid> _animals = new();
    public IReadOnlyCollection<Guid> AnimalIds => _animals.AsReadOnly();

    public Enclosure(Guid id, EnclosureType type, double sizeSqM, int maxCapacity)
    {
        Id = id;
        Type = type;
        SizeSqM = sizeSqM;
        MaxCapacity = maxCapacity;
    }

    public void AddAnimal(Animal animal)
    {
        if (_animals.Count >= MaxCapacity)
            throw new InvalidOperationException("Вольер заполнен.");

        _animals.Add(animal.Id);
        animal.MoveTo(Id);
    }

    public void RemoveAnimal(Animal animal)
    {
        _animals.Remove(animal.Id);
    }

    public void Clean() { /* дом. правило: фиксируем дату уборки и т.д.*/ }
}