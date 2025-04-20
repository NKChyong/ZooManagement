using ZooManagement.Application.Interfaces;
using ZooManagement.Domain.Entities;
using ZooManagement.Infrastructure.Persistence;

namespace ZooManagement.Infrastructure.Repositories;

/// <summary>
/// Простая in‑memory реализация IEnclosureRepository.
/// Хранит данные в синглтон‑контексте InMemoryDbContext.
/// </summary>
internal sealed class InMemoryEnclosureRepository : IEnclosureRepository
{
    private readonly InMemoryDbContext _db;

    public InMemoryEnclosureRepository(InMemoryDbContext db) => _db = db;

    public Task AddAsync(Enclosure enclosure)
    {
        _db.Enclosures.Add(enclosure);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Enclosure>> GetAllAsync()
        => Task.FromResult((IReadOnlyCollection<Enclosure>)_db.Enclosures);

    public Task<Enclosure?> GetAsync(Guid id)
        => Task.FromResult(_db.Enclosures.SingleOrDefault(e => e.Id == id));

    public Task RemoveAsync(Guid id)
    {
        var enc = _db.Enclosures.SingleOrDefault(e => e.Id == id);
        if (enc is not null) _db.Enclosures.Remove(enc);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
    {
        // для in‑memory ничего не нужно делать
        return Task.CompletedTask;
    }
}