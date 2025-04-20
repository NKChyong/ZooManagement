// InMemoryAnimalRepository.cs
using ZooManagement.Application.Interfaces;
using ZooManagement.Domain.Entities;
using ZooManagement.Infrastructure.Persistence;

namespace ZooManagement.Infrastructure.Repositories;

internal sealed class InMemoryAnimalRepository : IAnimalRepository
{
    private readonly InMemoryDbContext _db;
    public InMemoryAnimalRepository(InMemoryDbContext db) => _db = db;

    public Task AddAsync(Animal animal) { _db.Animals.Add(animal); return Task.CompletedTask; }

    public Task<IReadOnlyCollection<Animal>> GetAllAsync()
        => Task.FromResult((IReadOnlyCollection<Animal>)_db.Animals);

    public Task<Animal?> GetAsync(Guid id)
        => Task.FromResult(_db.Animals.SingleOrDefault(a => a.Id == id));

    public Task RemoveAsync(Guid id)
    {
        var a = _db.Animals.SingleOrDefault(a => a.Id == id);
        if (a is not null) _db.Animals.Remove(a);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
    {
        // in‑memory: ничего не делаем
        return Task.CompletedTask;
    }
}