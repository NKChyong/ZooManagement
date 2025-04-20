using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZooManagement.Domain.Entities;
using ZooManagement.Domain.Enums;
using ZooManagement.Domain.ValueObjects;
using ZooManagement.Infrastructure.Persistence;
using ZooManagement.Infrastructure.Repositories;

namespace ZooManagement.Tests.Repositories
{
    public class InMemoryAnimalRepositoryTests
    {
        private readonly InMemoryDbContext _db = new();

        private InMemoryAnimalRepository CreateRepo() =>
            new InMemoryAnimalRepository(_db);

        [Fact]
        public async Task AddAndGetAll_ReturnsAddedAnimals()
        {
            // arrange
            var repo = CreateRepo();
            var a1 = new Animal(Guid.NewGuid(), new Species("A"), new AnimalName("One"),
                                DateTime.UtcNow.AddYears(-1), Gender.Unknown, new Food("X"));
            var a2 = new Animal(Guid.NewGuid(), new Species("B"), new AnimalName("Two"),
                                DateTime.UtcNow.AddYears(-2), Gender.Male, new Food("Y"));

            // act
            await repo.AddAsync(a1);
            await repo.AddAsync(a2);
            await repo.SaveChangesAsync();

            var all = (await repo.GetAllAsync()).ToList();

            // assert
            Assert.Contains(a1, all);
            Assert.Contains(a2, all);
            Assert.Equal(2, all.Count);
        }

        [Fact]
        public async Task GetAsync_ReturnsCorrectAnimalOrNull()
        {
            // arrange
            var repo = CreateRepo();
            var id = Guid.NewGuid();
            var animal = new Animal(id, new Species("C"), new AnimalName("Three"),
                                    DateTime.UtcNow, Gender.Female, new Food("Z"));
            await repo.AddAsync(animal);
            await repo.SaveChangesAsync();

            // act & assert
            var found = await repo.GetAsync(id);
            Assert.NotNull(found);
            Assert.Equal(id, found!.Id);

            var missing = await repo.GetAsync(Guid.NewGuid());
            Assert.Null(missing);
        }

        [Fact]
        public async Task RemoveAsync_DeletesAnimal()
        {
            // arrange
            var repo = CreateRepo();
            var animal = new Animal(Guid.NewGuid(), new Species("D"), new AnimalName("Four"),
                                    DateTime.UtcNow, Gender.Male, new Food("W"));
            await repo.AddAsync(animal);
            await repo.SaveChangesAsync();

            // act
            await repo.RemoveAsync(animal.Id);
            await repo.SaveChangesAsync();
            var all = await repo.GetAllAsync();

            // assert
            Assert.DoesNotContain(all, a => a.Id == animal.Id);
        }
    }
}
