using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZooManagement.Domain.Entities;
using ZooManagement.Domain.ValueObjects;
using ZooManagement.Infrastructure.Persistence;
using ZooManagement.Infrastructure.Repositories;

namespace ZooManagement.Tests.Repositories
{
    public class InMemoryFeedingScheduleRepositoryTests
    {
        private readonly InMemoryDbContext _db = new();
        private InMemoryFeedingScheduleRepository CreateRepo() =>
            new InMemoryFeedingScheduleRepository(_db);

        [Fact]
        public async Task AddAndGetAll_ReturnsAddedSchedules()
        {
            var repo = CreateRepo();
            var s1 = new FeedingSchedule(Guid.NewGuid(), Guid.NewGuid(),
                                         DateTime.UtcNow.AddHours(1), new Food("F1"));
            var s2 = new FeedingSchedule(Guid.NewGuid(), Guid.NewGuid(),
                                         DateTime.UtcNow.AddHours(2), new Food("F2"));

            await repo.AddAsync(s1);
            await repo.AddAsync(s2);
            await repo.SaveChangesAsync();

            var all = (await repo.GetAllAsync()).ToList();
            Assert.Contains(s1, all);
            Assert.Contains(s2, all);
            Assert.Equal(2, all.Count);
        }

        [Fact]
        public async Task GetAsync_ReturnsCorrectOrNull()
        {
            var repo = CreateRepo();
            var id = Guid.NewGuid();
            var sched = new FeedingSchedule(id, Guid.NewGuid(),
                                            DateTime.UtcNow.AddMinutes(30), new Food("G"));

            await repo.AddAsync(sched);
            await repo.SaveChangesAsync();

            var found = await repo.GetAsync(id);
            Assert.NotNull(found);
            Assert.Equal(id, found!.Id);

            var missing = await repo.GetAsync(Guid.NewGuid());
            Assert.Null(missing);
        }

        [Fact]
        public async Task RemoveAsync_DeletesSchedule()
        {
            var repo = CreateRepo();
            var sched = new FeedingSchedule(Guid.NewGuid(), Guid.NewGuid(),
                                            DateTime.UtcNow, new Food("H"));

            await repo.AddAsync(sched);
            await repo.SaveChangesAsync();
            await repo.RemoveAsync(sched.Id);
            await repo.SaveChangesAsync();

            var all = await repo.GetAllAsync();
            Assert.DoesNotContain(all, s => s.Id == sched.Id);
        }
    }
}
