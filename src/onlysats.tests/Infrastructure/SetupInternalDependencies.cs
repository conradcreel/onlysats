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

    public static Mock<MessagePublisherProxy> SetupMessagePublisher()
    {
        var mockDaprClient = new Mock<MessagePublisherProxy>();
        
        mockDaprClient.Setup(s =>
            s.PublishEvent(It.IsAny<string>(), It.IsAny<It.IsAnyType>(), It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);

        return mockDaprClient;
    }
}