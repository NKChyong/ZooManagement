using ZooManagement.Application.Interfaces;
using ZooManagement.Domain.Events;

namespace ZooManagement.Infrastructure.Messaging;

internal sealed class SimpleEventPublisher : IEventPublisher
{
    public Task PublishAsync(IDomainEvent domainEvent, CancellationToken ct = default)
    {
        Console.WriteLine($"[EVENT] {domainEvent.GetType().Name} fired at {domainEvent.OccurredOn:u}");
        return Task.CompletedTask;
    }
}