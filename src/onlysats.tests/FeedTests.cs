using Xunit;
using FluentAssertions;
using Moq;
using onlysats.domain.Services.Repositories;
using onlysats.domain.Services;

namespace onlysats.tests;

public class FeedTests
{
    private Mock<IFeedRepository> _MockFeedRepository;

    private IFeedService _FeedService;

    public FeedTests()
    {
        _MockFeedRepository = new Mock<IFeedRepository>();

        Setup();

        _FeedService = new FeedService(
            feedRepository: _MockFeedRepository.Object
        );
    }

    private void Setup()
    {
        Assert.NotNull(_MockFeedRepository);

        // TODO: Setup Feed Repository
    }
    
    [Fact]
    public void Test1()
    {
        Assert.Equal(1,1);
    }
}