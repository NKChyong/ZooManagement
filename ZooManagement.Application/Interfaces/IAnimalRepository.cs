// IAnimalRepository.cs
using ZooManagement.Domain.Entities;

namespace ZooManagement.Application.Interfaces;
public interface IAnimalRepository
{
    Task<Animal?> GetAsync(Guid id);
    Task<IReadOnlyCollection<Animal>> GetAllAsync();
    Task AddAsync(Animal animal);
    Task RemoveAsync(Guid id);
    Task SaveChangesAsync();
}
