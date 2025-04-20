using ZooManagement.Domain.Entities;

namespace ZooManagement.Application.Interfaces;

/// <summary>
/// Контракт репозитория для работы с вольерами.
/// Инфраструктурный слой должен предоставить реализацию.
/// </summary>
public interface IEnclosureRepository
{
    Task<Enclosure?>              GetAsync(Guid id);
    Task<IReadOnlyCollection<Enclosure>> GetAllAsync();

    Task AddAsync(Enclosure enclosure);
    Task RemoveAsync(Guid id);

    /// <summary>Сохранить накопленные изменения (для in‑memory ничего не делает).</summary>
    Task SaveChangesAsync();
}