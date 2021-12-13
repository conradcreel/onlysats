using System.Threading;
using System.Threading.Tasks;
using Dapr.Client;
using Moq;
using onlysats.domain.Models;

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

    public static Mock<DaprClient> SetupDaprClient()
    {
        var mockDaprClient = new Mock<DaprClient>();
        
        mockDaprClient.Setup(s =>
            s.PublishEventAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<It.IsAnyType>(), It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);

        return mockDaprClient;
    }
}