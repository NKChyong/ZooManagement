using ZooManagement.Application.Interfaces;
using ZooManagement.Domain.Entities;
using ZooManagement.Infrastructure.Persistence;

namespace ZooManagement.Infrastructure.Repositories;

/// <summary>
/// In‑memory реализация IFeedingScheduleRepository.
/// </summary>
internal sealed class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
{
    private readonly InMemoryDbContext _db;

    public InMemoryFeedingScheduleRepository(InMemoryDbContext db) => _db = db;

    public Task AddAsync(FeedingSchedule schedule)
    {
        _db.FeedingSchedules.Add(schedule);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<FeedingSchedule>> GetAllAsync()
        => Task.FromResult((IReadOnlyCollection<FeedingSchedule>)_db.FeedingSchedules);

    public Task<FeedingSchedule?> GetAsync(Guid id)
        => Task.FromResult(_db.FeedingSchedules.SingleOrDefault(s => s.Id == id));

    public Task RemoveAsync(Guid id)
    {
        var fs = _db.FeedingSchedules.SingleOrDefault(s => s.Id == id);
        if (fs is not null) _db.FeedingSchedules.Remove(fs);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
    {
        // persist‑нет: действий не требуется
        return Task.CompletedTask;
    }
}