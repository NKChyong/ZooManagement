using ZooManagement.Domain.Entities;

namespace ZooManagement.Application.Interfaces;

/// <summary>
/// Контракт репозитория для расписаний кормления.
/// </summary>
public interface IFeedingScheduleRepository
{
    Task<FeedingSchedule?>              GetAsync(Guid id);
    Task<IReadOnlyCollection<FeedingSchedule>> GetAllAsync();

    Task AddAsync(FeedingSchedule schedule);
    Task RemoveAsync(Guid id);

    Task SaveChangesAsync();
}