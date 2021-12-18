using Xunit;
using FluentAssertions;
using Moq;
using onlysats.domain.Services;
using onlysats.domain.Services.Repositories;
using Dapr.Client;
using onlysats.tests.Infrastructure;

namespace onlysats.tests;

public class UserEngagementTests
{
    private Mock<INotificationRepository> _MockNotificationRepository;
    private Mock<IMessagePublisher> _MockMessagePublisher;

    private IUserEngagementService _UserEngagementService;

    public UserEngagementTests()
    {
        _MockMessagePublisher = SetupInternalDependencies.SetupMessagePublisher();
        _MockNotificationRepository = new Mock<INotificationRepository>();

        Setup();

        _UserEngagementService = new UserEngagementService(
            notificationRepository: _MockNotificationRepository.Object,
            messagePublisher: _MockMessagePublisher.Object
        );
    }

    private void Setup()
    {
        Assert.NotNull(_MockNotificationRepository);
        Assert.NotNull(_MockMessagePublisher);

        // TODO: Setup Notification Repository
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal(1, 1);
    }
}