using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ZooManagement.Application.Interfaces;
using ZooManagement.Domain.Events;
using ZooManagement.Infrastructure.Messaging;

namespace ZooManagement.Tests.Infrastructure
{
    public class SimpleEventPublisherTests
    {
        [Fact]
        public async Task PublishAsync_CompletesSuccessfully()
        {
            IEventPublisher pub = new SimpleEventPublisher();
            var evt = new AnimalMovedEvent(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), DateTime.UtcNow);
            // should not throw, should complete
            await pub.PublishAsync(evt, CancellationToken.None);
        }

        [Fact]
        public void ImplementsIEventPublisher()
        {
            var pub = new SimpleEventPublisher();
            Assert.IsAssignableFrom<IEventPublisher>(pub);
        }
    }
}
