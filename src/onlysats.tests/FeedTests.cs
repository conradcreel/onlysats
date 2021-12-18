using Xunit;
using Moq;
using onlysats.domain.Services.Repositories;
using onlysats.domain.Services;
using onlysats.tests.Infrastructure;

namespace onlysats.tests;

public class FeedTests
{
    private Mock<IFeedRepository> _MockFeedRepository;
    private Mock<IMessagePublisher> _MockMessagePublisher;
    private IFeedService _FeedService;

    public FeedTests()
    {
        _MockFeedRepository = new Mock<IFeedRepository>();
        _MockMessagePublisher = SetupInternalDependencies.SetupMessagePublisher();

        Setup();

        _FeedService = new FeedService(
            feedRepository: _MockFeedRepository.Object,
            messagePublisher: _MockMessagePublisher.Object
        );
    }

    private void Setup()
    {
        Assert.NotNull(_MockFeedRepository);
        Assert.NotNull(_MockMessagePublisher);

        // TODO: Setup Feed Repository
    }
    
    [Fact]
    public void Test1()
    {
        Assert.Equal(1,1);
    }
}