using Xunit;
using FluentAssertions;
using Moq;
using onlysats.domain.Services.Repositories;
using onlysats.domain.Services;
using Dapr.Client;
using onlysats.tests.Infrastructure;

namespace onlysats.tests;

public class FeedTests
{
    private Mock<IFeedRepository> _MockFeedRepository;
    private Mock<DaprClient> _MockDaprClient;

    private IFeedService _FeedService;

    public FeedTests()
    {
        _MockFeedRepository = new Mock<IFeedRepository>();
        _MockDaprClient = SetupInternalDependencies.SetupDaprClient();

        Setup();

        _FeedService = new FeedService(
            feedRepository: _MockFeedRepository.Object,
            daprClient: _MockDaprClient.Object
        );
    }

    private void Setup()
    {
        Assert.NotNull(_MockFeedRepository);
        Assert.NotNull(_MockDaprClient);

        // TODO: Setup Feed Repository
    }
    
    [Fact]
    public void Test1()
    {
        Assert.Equal(1,1);
    }
}