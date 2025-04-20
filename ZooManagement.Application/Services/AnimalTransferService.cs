// AnimalTransferService.cs
using ZooManagement.Application.Interfaces;
using ZooManagement.Domain.Entities;

namespace ZooManagement.Application.Services;

public sealed class AnimalTransferService
{
    private readonly IAnimalRepository _animals;
    private readonly IEnclosureRepository _enclosures;

    public AnimalTransferService(IAnimalRepository animals,
        IEnclosureRepository enclosures)
    {
        _animals = animals;
        _enclosures = enclosures;
    }

    public async Task MoveAsync(Guid animalId, Guid toEnclosureId)
    {
        var animal = await _animals.GetAsync(animalId)
                     ?? throw new KeyNotFoundException("Животное не найдено");
        var toEnclosure = await _enclosures.GetAsync(toEnclosureId)
                          ?? throw new KeyNotFoundException("Вольер не найден");

        // убираем из старого
        if (animal.EnclosureId is { } fromId)
        {
            var fromEnc = await _enclosures.GetAsync(fromId);
            fromEnc?.RemoveAnimal(animal);
        }

        toEnclosure.AddAnimal(animal);

        await _animals.SaveChangesAsync();
        await _enclosures.SaveChangesAsync();
    }
}