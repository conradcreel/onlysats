using System.Threading;
using System.Threading.Tasks;
using Moq;
using onlysats.domain.Models;
using onlysats.domain.Services;

namespace onlysats.tests.Infrastructure;

public static class SetupInternalDependencies
{
    public static OnlySatsConfiguration SetupConfiguration()
    {
        return new OnlySatsConfiguration
        {
            // TODO
        };
    }

    public static Mock<IMessagePublisher> SetupMessagePublisher()
    {
        var mockMessagePublisher = new Mock<IMessagePublisher>();
        
        mockMessagePublisher.Setup(s =>
            s.PublishEvent(It.IsAny<string>(), It.IsAny<It.IsAnyType>(), It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);

        return mockMessagePublisher;
    }
}