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
    public class InMemoryEnclosureRepositoryTests
    {
        private readonly InMemoryDbContext _db = new();
        private InMemoryEnclosureRepository CreateRepo() =>
            new InMemoryEnclosureRepository(_db);

        [Fact]
        public async Task AddAndGetAll_ReturnsAddedEnclosures()
        {
            var repo = CreateRepo();
            var e1 = new Enclosure(Guid.NewGuid(), new EnclosureType("E1"), 10, 2);
            var e2 = new Enclosure(Guid.NewGuid(), new EnclosureType("E2"), 20, 3);

            await repo.AddAsync(e1);
            await repo.AddAsync(e2);
            await repo.SaveChangesAsync();

            var all = (await repo.GetAllAsync()).ToList();
            Assert.Contains(e1, all);
            Assert.Contains(e2, all);
            Assert.Equal(2, all.Count);
        }

        [Fact]
        public async Task GetAsync_ReturnsCorrectOrNull()
        {
            var repo = CreateRepo();
            var id = Guid.NewGuid();
            var enc = new Enclosure(id, new EnclosureType("X"), 5, 1);

            await repo.AddAsync(enc);
            await repo.SaveChangesAsync();

            var found = await repo.GetAsync(id);
            Assert.NotNull(found);
            Assert.Equal(id, found!.Id);

            var missing = await repo.GetAsync(Guid.NewGuid());
            Assert.Null(missing);
        }

        [Fact]
        public async Task RemoveAsync_DeletesEnclosure()
        {
            var repo = CreateRepo();
            var enc = new Enclosure(Guid.NewGuid(), new EnclosureType("Y"), 8, 2);

            await repo.AddAsync(enc);
            await repo.SaveChangesAsync();
            await repo.RemoveAsync(enc.Id);
            await repo.SaveChangesAsync();

            var all = await repo.GetAllAsync();
            Assert.DoesNotContain(all, e => e.Id == enc.Id);
        }
    }
}
