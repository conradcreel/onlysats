using Xunit;
using FluentAssertions;
using Moq;
using onlysats.domain.Services;
using onlysats.domain.Services.Repositories;
using onlysats.tests.Infrastructure;
using Dapr.Client;
using onlysats.domain.Services.Request.Onboarding;
using System.Threading.Tasks;
using onlysats.domain.Entity;
using onlysats.domain.Models;

namespace onlysats.tests;

public class OnboardingTests
{
    private readonly int EXISTING_USER_ACCOUNT_ID = 100;
    private readonly int EXISTING_CREATOR_ID = 200;
    private readonly int EXISTING_PATRON_ID = 300;
    private readonly int NONEXISTING_USER_ACCOUNT_ID = 1;
    private readonly int NONEXISTING_CREATOR_ID = 2;
    private readonly int NONEXISTING_PATRON_ID = 3;

    private Mock<IUserAccountRepository> _MockUserAccountRepository;
    private Mock<ICreatorRepository> _MockCreatorRepository;
    private Mock<IPatronRepository> _MockPatronRepository;
    private Mock<MessagePublisherProxy> _MockMessagePublisher;

    private IOnboardingService _OnboardingService;

    public OnboardingTests()
    {
        _MockUserAccountRepository = new Mock<IUserAccountRepository>();
        _MockCreatorRepository = new Mock<ICreatorRepository>();
        _MockPatronRepository = new Mock<IPatronRepository>();
        _MockMessagePublisher = SetupInternalDependencies.SetupMessagePublisher();

        Setup();

        _OnboardingService = new OnboardingService(
            userAccountRepository: _MockUserAccountRepository.Object,
            creatorRepository: _MockCreatorRepository.Object,
            patronRepository: _MockPatronRepository.Object,
            messagePublisher: _MockMessagePublisher.Object
        );
    }

    private UserAccount GetExistingUserAccount() => new UserAccount
    {
        // TODO
    };

    private Creator GetExistingCreator() => new Creator
    {
        // TODO
    };

    private CreatorModel GetExistingCreatorModel() => new CreatorModel
    {
        // TODO
    };

    private CreatorAccountSettings GetExistingCreatorAccountSettings() => new CreatorAccountSettings
    {
        // TODO
    };

    private CreatorChatSettings GetExistingCreatorChatSettings() => new CreatorChatSettings
    {
        // TODO
    };

    

    private Patron GetExistingPatron() => new Patron
    {
        // TODO
    };

    private PatronModel GetExistingPatronModel() => new PatronModel
    {
        // TODO
    };

    private void Setup()
    {
        Assert.NotNull(_MockUserAccountRepository);
        Assert.NotNull(_MockCreatorRepository);
        Assert.NotNull(_MockPatronRepository);
        Assert.NotNull(_MockMessagePublisher);

        _MockUserAccountRepository
            .Setup(u => u.GetUserAccount(It.IsAny<int>()))
            .ReturnsAsync((int userId) =>
            {
                if (userId == EXISTING_USER_ACCOUNT_ID)
                {
                    return GetExistingUserAccount();
                }

                return default;
            });

        _MockUserAccountRepository
            .Setup(u => u.UpsertUserAccount(It.IsAny<UserAccount>()))
            .ReturnsAsync((UserAccount userAccount) =>
            {
                if (userAccount.Id == 0)
                {
                    userAccount.Id = EXISTING_USER_ACCOUNT_ID;
                }

                return userAccount;
            });

        _MockCreatorRepository
            .Setup(c => c.GetCreator(It.IsAny<int>()))
            .ReturnsAsync((int creatorId) =>
            {
                if (creatorId == EXISTING_CREATOR_ID)
                {
                    return GetExistingCreator();
                }

                return default;
            });

        _MockCreatorRepository
            .Setup(c => c.GetCreatorDetail(It.IsAny<int>()))
            .ReturnsAsync((int creatorId) =>
            {
                if (creatorId == EXISTING_CREATOR_ID)
                {
                    return GetExistingCreatorModel();
                }

                return default;
            });

        _MockCreatorRepository
            .Setup(c => c.GetCreatorAccountSettings(It.IsAny<int>()))
            .ReturnsAsync((int creatorId) =>
            {
                // TODO
                return null;
            });

        _MockCreatorRepository
            .Setup(c => c.GetCreatorChatSettings(It.IsAny<int>()))
            .ReturnsAsync((int creatorId) =>
            {
                // TODO
                return null;
            });

        _MockCreatorRepository
            .Setup(c => c.GetCreatorNotificationSettings(It.IsAny<int>()))
            .ReturnsAsync((int creatorId) =>
            {
                // TODO
                return null;
            });

        _MockCreatorRepository
            .Setup(c => c.GetCreatorProfileSettings(It.IsAny<int>()))
            .ReturnsAsync((int creatorId) =>
            {
                // TODO
                return null;
            });

        _MockCreatorRepository
            .Setup(c => c.GetCreatorSecuritySettings(It.IsAny<int>()))
            .ReturnsAsync((int creatorId) =>
            {
                // TODO
                return null;
            });

        _MockCreatorRepository
            .Setup(c => c.UpsertCreator(It.IsAny<Creator>()))
            .ReturnsAsync((Creator c) =>
            {
                // TODO
                return c;
            });

        _MockCreatorRepository
            .Setup(c => c.UpsertCreatorAccountSettings(It.IsAny<CreatorAccountSettings>()))
            .ReturnsAsync((CreatorAccountSettings c) =>
            {
                // TODO
                return c;
            });

        _MockCreatorRepository
            .Setup(c => c.UpsertCreatorChatSettings(It.IsAny<CreatorChatSettings>()))
            .ReturnsAsync((CreatorChatSettings c) =>
            {
                // TODO
                return c;
            });

        _MockCreatorRepository
            .Setup(c => c.UpsertCreatorNotificationSettings(It.IsAny<CreatorNotificationSettings>()))
            .ReturnsAsync((CreatorNotificationSettings c) =>
            {
                // TODO
                return c;
            });

        _MockCreatorRepository
            .Setup(c => c.UpsertCreatorProfileSettings(It.IsAny<CreatorProfileSettings>()))
            .ReturnsAsync((CreatorProfileSettings c) =>
            {
                // TODO
                return c;
            });

        _MockCreatorRepository
            .Setup(c => c.UpsertCreatorSecuritySettings(It.IsAny<CreatorSecuritySettings>()))
            .ReturnsAsync((CreatorSecuritySettings c) =>
            {
                // TODO
                return c;
            });

        _MockPatronRepository
            .Setup(p => p.GetPatronDetail(It.IsAny<int>()))
            .ReturnsAsync((int patronId) =>
            {
                // TODO
                return null;
            });

        _MockPatronRepository
            .Setup(p => p.GetPatron(It.IsAny<int>()))
            .ReturnsAsync((int patronId) =>
            {
                // TODO
                return null;
            });

        _MockPatronRepository
            .Setup(p => p.UpsertPatron(It.IsAny<Patron>()))
            .ReturnsAsync((Patron patron) =>
            {
                // TODO
                return patron;
            });
    }

    [Fact]
    public async Task Can_Create_New_Creator()
    {
        var request = new SetupCreatorRequest
        {
            // TODO
        };

        var response = await _OnboardingService.SetupCreator(request);

        response.Should().NotBeNull();
        response.ResponseDetails.Should().NotBeNull();
        response?.ResponseDetails?.IsSuccess.Should().BeTrue();
        response?.UserAccountId.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task When_Creating_New_Creator_Must_Supply_UserAccount_Properties()
    {

    }

    [Fact]
    public async Task Can_Retrieve_Creator_Details()
    {

    }

    [Fact]
    public async Task Can_Create_New_Patron()
    {

    }

    [Fact]
    public async Task When_Creating_New_Patron_Must_Supply_UserAccount_Properties()
    {

    }

    [Fact]
    public async Task Can_Retrieve_Patron_Details()
    {

    }

    [Fact]
    public async Task Can_Update_Creator_Account_Settings()
    {

    }

    [Fact]
    public async Task Can_Update_Creator_Chat_Settings()
    {

    }

    [Fact]
    public async Task Can_Update_Creator_Notification_Settings()
    {

    }

    [Fact]
    public async Task Can_Update_Creator_Profile_Settings()
    {

    }

    [Fact]
    public async Task Can_Update_Creator_Security_Settings()
    {

    }
}