using Xunit;
using FluentAssertions;
using Moq;
using onlysats.domain.Services;
using onlysats.domain.Services.Repositories;

namespace onlysats.tests;

public class UserEngagementTests
{
    private Mock<INotificationRepository> _MockNotificationRepository;
    
    private IUserEngagementService _UserEngagementService;

    public UserEngagementTests()
    {
        _MockNotificationRepository = new Mock<INotificationRepository>();

        Setup();

        _UserEngagementService = new UserEngagementService(
            notificationRepository: _MockNotificationRepository.Object
        );
    }

    private void Setup()
    {
        Assert.NotNull(_MockNotificationRepository);

        // TODO: Setup Notification Repository
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal(1, 1);
    }
}