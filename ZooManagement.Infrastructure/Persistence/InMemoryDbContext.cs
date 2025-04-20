// InMemoryDbContext.cs
using ZooManagement.Domain.Entities;

namespace ZooManagement.Infrastructure.Persistence;

internal sealed class InMemoryDbContext
{
    public List<Animal> Animals { get; } = new();
    public List<Enclosure> Enclosures { get; } = new();
    public List<FeedingSchedule> FeedingSchedules { get; } = new();
}