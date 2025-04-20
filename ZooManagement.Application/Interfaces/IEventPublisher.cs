// IEventPublisher.cs
using ZooManagement.Domain.Events;

namespace ZooManagement.Application.Interfaces;
public interface IEventPublisher
{
    Task PublishAsync(IDomainEvent domainEvent, CancellationToken ct = default);
}