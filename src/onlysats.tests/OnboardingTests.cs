using System;
using Xunit;
using FluentAssertions;
using Moq;
using onlysats.domain.Services;
using onlysats.domain.Services.Repositories;
using onlysats.tests.Infrastructure;
using onlysats.domain.Services.Request.Onboarding;
using System.Threading.Tasks;
using onlysats.domain.Entity;
using onlysats.domain.Models;
using onlysats.domain.Enums;
using onlysats.domain.Constants;
using onlysats.domain.Services.Request;

namespace onlysats.tests;

public class OnboardingTests
{
    private readonly int EXISTING_CREATOR_USER_ACCOUNT_ID = 100;
    private readonly int EXISTING_PATRON_USER_ACCOUNT_ID = 101;
    private readonly int EXISTING_CREATOR_ID = 200;
    private readonly int EXISTING_PATRON_ID = 300;
    private readonly int NONEXISTING_USER_ACCOUNT_ID = 1;
    private readonly int NONEXISTING_CREATOR_ID = 2;
    private readonly int NONEXISTING_PATRON_ID = 3;

    private Mock<IUserAccountRepository> _MockUserAccountRepository;
    private Mock<ICreatorRepository> _MockCreatorRepository;
    private Mock<IPatronRepository> _MockPatronRepository;
    private Mock<IMessagePublisher> _MockMessagePublisher;

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

    // Use this for all requests where you want to mock an authenticated and authorized Creator
    private AuthenticatedUserContext GetValidCreatorUserContext() => new AuthenticatedUserContext
    {
        UserAccountId = EXISTING_CREATOR_USER_ACCOUNT_ID,
        Username = "satoshi",
        UserRole = EUserRole.CREATOR
    };

    // Use this for all requests where you want to mock an authenticated and authorized Patron
    private AuthenticatedUserContext GetValidPatronUserContext() => new AuthenticatedUserContext
    {
        UserAccountId = EXISTING_PATRON_USER_ACCOUNT_ID,
        Username = "yoyo",
        UserRole = EUserRole.PATRON
    };

    // Use this for all requests where you want to mock an authenticated but unauthorized Creator
    private AuthenticatedUserContext GetInvalidCreatorUserContext() => new AuthenticatedUserContext
    {
        UserAccountId = EXISTING_CREATOR_USER_ACCOUNT_ID + 1,
        Username = "satoshi",
        UserRole = EUserRole.CREATOR
    };

    // Use this for all requests where you want to mock an authenticated but unauthorized Patron
    private AuthenticatedUserContext GetInvalidPatronUserContext() => new AuthenticatedUserContext
    {
        UserAccountId = EXISTING_PATRON_USER_ACCOUNT_ID + 1,
        Username = "yoyo",
        UserRole = EUserRole.PATRON
    };

    private UserAccount GetExistingUserAccount(int userAccountId) => new UserAccount
    {
        Id = userAccountId,
        Username = "satoshi",
        Email = "satoshi@bitcoin.org",
        Role = userAccountId == EXISTING_CREATOR_USER_ACCOUNT_ID ? EUserRole.CREATOR : EUserRole.PATRON,
        UserId = "user290919",
        IdpSource = "Auth0",
        DateAddedUtc = DateTime.UtcNow,
        DateUpdatedUtc = DateTime.UtcNow
    };

    private Creator GetExistingCreator() => new Creator
    {
        Id = EXISTING_CREATOR_ID,
        UserAccountId = EXISTING_CREATOR_USER_ACCOUNT_ID
    };

    private CreatorModel GetExistingCreatorModel() => new CreatorModel
    {
        UserAccountId = EXISTING_CREATOR_USER_ACCOUNT_ID,
        CreatorId = EXISTING_CREATOR_ID,
        Username = "satoshi",
        Email = "satoshi@bitcoin.org",
        DisplayName = "Satoshi Nakamoto",
        CoverPhotoUri = "https://3i23jkjk.blobstore.com/32kdfk3242.png",
        ProfilePhotoUri = "https://3234525askks.blobstore.com/383489sadhf.png",
        SubscriptionPricePerMonth = 9_99,
        AboutHtml = "<p>All about Satoshi</p>",
        AmazonWishList = "https://www.amazon.com/wishlists/34kkdj3993dj",
        ChatSettings = new ChatSettingsModel
        {
            ShowWelcomeMessage = true,
            HideOutgoingMassMessages = true
        },
        NotificationSettings = new NotificationSettingsModel
        {
            UsePush = true,
            UseEmail = true,
            NewMessage = true,
            NewSubscriber = true,
            NewTip = true,
            NewPurchase = true
        },
        SecuritySettings = new SecuritySettingsModel
        {
            ShowActivityStatus = true,
            FullyPrivateProfile = false,
            ShowPatronCount = false,
            ShowMediaCount = false,
            WatermarkPhotos = false,
            WatermarkVideos = false
        }
    };

    private CreatorChatSettings GetExistingCreatorChatSettings() => new CreatorChatSettings
    {
        Id = 1,
        CreatorId = EXISTING_CREATOR_ID,
        ShowWelcomeMessage = true,
        HideOutgoingMassMessages = true,
        DateAddedUtc = DateTime.UtcNow,
        DateUpdatedUtc = DateTime.UtcNow
    };

    private CreatorNotificationSettings GetExistingCreatorNotificationSettings() => new CreatorNotificationSettings
    {
        Id = 1,
        CreatorId = EXISTING_CREATOR_ID,
        UsePush = true,
        UseEmail = true,
        NewMessage = true,
        NewSubscriber = true,
        NewTip = true,
        NewPurchase = true,
        DateAddedUtc = DateTime.UtcNow,
        DateUpdatedUtc = DateTime.UtcNow
    };

    private CreatorProfileSettings GetExistingCreatorProfileSettings() => new CreatorProfileSettings
    {
        Id = 1,
        CreatorId = EXISTING_CREATOR_ID,
        DisplayName = "Satoshi Nakamoto",
        CoverPhotoUri = "https://3i23jkjk.blobstore.com/32kdfk3242.png",
        ProfilePhotoUri = "https://3234525askks.blobstore.com/383489sadhf.png",
        SubscriptionPricePerMonth = 9_99,
        AboutHtml = "<p>All about Satoshi</p>",
        AmazonWishList = "https://www.amazon.com/wishlists/34kkdj3993dj",
        DateAddedUtc = DateTime.UtcNow,
        DateUpdatedUtc = DateTime.UtcNow
    };

    private CreatorSecuritySettings GetExistingCreatorSecuritySettings() => new CreatorSecuritySettings
    {
        Id = 1,
        CreatorId = EXISTING_CREATOR_ID,
        ShowActivityStatus = true,
        FullyPrivateProfile = false,
        ShowPatronCount = true,
        ShowMediaCount = true,
        WatermarkPhotos = false,
        WatermarkVideos = false,
        DateAddedUtc = DateTime.UtcNow,
        DateUpdatedUtc = DateTime.UtcNow
    };

    private Patron GetExistingPatron() => new Patron
    {
        Id = EXISTING_PATRON_ID,
        UserAccountId = EXISTING_PATRON_USER_ACCOUNT_ID,
        MemberUntil = DateTime.UtcNow.AddMonths(1),
        DateAddedUtc = DateTime.UtcNow,
        DateUpdatedUtc = DateTime.UtcNow
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
            .ReturnsAsync((int userAccountId) =>
            {
                if (userAccountId == EXISTING_CREATOR_USER_ACCOUNT_ID ||
                    userAccountId == EXISTING_PATRON_USER_ACCOUNT_ID)
                {
                    return GetExistingUserAccount(userAccountId);
                }

                return default;
            });

        _MockUserAccountRepository
            .Setup(u => u.UpsertUserAccount(It.IsAny<UserAccount>()))
            .ReturnsAsync((UserAccount userAccount) =>
            {
                if (userAccount.Id == 0)
                {
                    if (userAccount.Role == EUserRole.CREATOR)
                    {
                        userAccount.Id = EXISTING_CREATOR_USER_ACCOUNT_ID;
                    }
                    else
                    {
                        userAccount.Id = EXISTING_PATRON_USER_ACCOUNT_ID;
                    }
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
            .Setup(c => c.GetCreatorChatSettings(It.IsAny<int>()))
            .ReturnsAsync((int creatorId) =>
            {
                if (creatorId == EXISTING_CREATOR_ID)
                {
                    return GetExistingCreatorChatSettings();
                }

                return default;
            });

        _MockCreatorRepository
            .Setup(c => c.GetCreatorNotificationSettings(It.IsAny<int>()))
            .ReturnsAsync((int creatorId) =>
            {
                if (creatorId == EXISTING_CREATOR_ID)
                {
                    return GetExistingCreatorNotificationSettings();
                }

                return default;
            });

        _MockCreatorRepository
            .Setup(c => c.GetCreatorProfileSettings(It.IsAny<int>()))
            .ReturnsAsync((int creatorId) =>
            {
                if (creatorId == EXISTING_CREATOR_ID)
                {
                    return GetExistingCreatorProfileSettings();
                }

                return default;
            });

        _MockCreatorRepository
            .Setup(c => c.GetCreatorSecuritySettings(It.IsAny<int>()))
            .ReturnsAsync((int creatorId) =>
            {
                if (creatorId == EXISTING_CREATOR_ID)
                {
                    return GetExistingCreatorSecuritySettings();
                }

                return default;
            });

        _MockCreatorRepository
            .Setup(c => c.UpsertCreator(It.IsAny<Creator>()))
            .ReturnsAsync((Creator c) =>
            {
                if (c.Id == 0)
                {
                    c.Id = EXISTING_CREATOR_ID;
                }

                return c;
            });

        _MockCreatorRepository
            .Setup(c => c.UpsertCreatorChatSettings(It.IsAny<CreatorChatSettings>()))
            .ReturnsAsync((CreatorChatSettings c) =>
            {
                return c;
            });

        _MockCreatorRepository
            .Setup(c => c.UpsertCreatorNotificationSettings(It.IsAny<CreatorNotificationSettings>()))
            .ReturnsAsync((CreatorNotificationSettings c) =>
            {
                return c;
            });

        _MockCreatorRepository
            .Setup(c => c.UpsertCreatorProfileSettings(It.IsAny<CreatorProfileSettings>()))
            .ReturnsAsync((CreatorProfileSettings c) =>
            {
                return c;
            });

        _MockCreatorRepository
            .Setup(c => c.UpsertCreatorSecuritySettings(It.IsAny<CreatorSecuritySettings>()))
            .ReturnsAsync((CreatorSecuritySettings c) =>
            {
                return c;
            });

        _MockPatronRepository
            .Setup(p => p.GetPatronDetail(It.IsAny<int>()))
            .ReturnsAsync((int patronId) =>
            {
                if (patronId == EXISTING_PATRON_ID)
                {
                    return GetExistingPatronModel();
                }

                return default;
            });

        _MockPatronRepository
            .Setup(p => p.GetPatron(It.IsAny<int>()))
            .ReturnsAsync((int patronId) =>
            {
                if (patronId == EXISTING_PATRON_ID)
                {
                    return GetExistingPatron();
                }

                return default;
            });

        _MockPatronRepository
            .Setup(p => p.UpsertPatron(It.IsAny<Patron>()))
            .ReturnsAsync((Patron patron) =>
            {
                if (patron.Id == 0)
                {
                    patron.Id = EXISTING_PATRON_ID;
                }

                return patron;
            });
    }

    [Fact]
    public async Task Can_Register_New_Creator()
    {
        var request = new SetupCreatorRequest
        {
            UserId = Guid.NewGuid().ToString(),
            IdpSource = "Auth0",
            Email = "somenewcreator@gmail.com",
            Username = "ignoring_it"
        };

        var response = await _OnboardingService.SetupCreator(request);

        response.Should().NotBeNull();
        response.ResponseDetails.Should().NotBeNull();
        response?.ResponseDetails?.IsSuccess.Should().BeTrue();
        response?.UserAccountId.Should().Be(EXISTING_CREATOR_USER_ACCOUNT_ID);
        response?.CreatorId.Should().Be(EXISTING_CREATOR_ID);
    }

    [Fact]
    public async Task Cannot_Register_Creator_With_Unsupported_IdP()
    {
        var request = new SetupCreatorRequest
        {
            IdpSource = Guid.NewGuid().ToString(),
            Email = "doesnot@matter.com",
            UserId = Guid.NewGuid().ToString(),
            Username = "doesnotmatter"
        };

        var response = await _OnboardingService.SetupCreator(request);

        response.Should().NotBeNull();
        response.ResponseDetails.Should().NotBeNull();
        response?.ResponseDetails?.IsSuccess.Should().BeFalse();
        response?.ResponseDetails?.StatusCode.Should().Be(CResponseStatus.BAD_REQUEST);
    }

    [Fact]
    public async Task When_Creating_New_Creator_Must_Supply_UserAccount_Properties()
    {
        var request = new SetupCreatorRequest
        {
            Email = "thisshouldfail@gmail.com",
            Username = "doesnotmatter"
        };

        var response = await _OnboardingService.SetupCreator(request);
        response.Should().NotBeNull();
        response.ResponseDetails.Should().NotBeNull();
        response?.ResponseDetails?.IsSuccess.Should().BeFalse();
        response?.ResponseDetails?.StatusCode.Should().Be(CResponseStatus.BAD_REQUEST);
    }

    [Fact]
    public async Task Can_Retrieve_Creator_Details()
    {
        var request = new LoadCreatorProfileRequest
        {
            CreatorId = EXISTING_CREATOR_ID,
            UserContext = GetValidCreatorUserContext()
        };

        var response = await _OnboardingService.LoadCreatorProfile(request);
        response.Should().NotBeNull();
        response.ResponseDetails.Should().NotBeNull();
        response?.ResponseDetails?.IsSuccess.Should().BeTrue();
        response?.Creator.Should().NotBeNull();
        response?.Creator?.CreatorId.Should().Be(EXISTING_CREATOR_ID);
    }
    /*
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
    */

    [Fact]
    public async Task Can_Update_Creator_Chat_Settings()
    {
        var request = new UpdateCreatorSettingsRequest
        {
            CreatorId = EXISTING_CREATOR_ID,
            ChatShowWelcomeMessage = new Include<bool>(false)
        };

        var response = await _OnboardingService.UpdateCreatorSettings(request);
        response.Should().NotBeNull();
        response.ResponseDetails.Should().NotBeNull();
        response?.ResponseDetails?.IsSuccess.Should().BeTrue();
        response?.UpdatedChatSettings.Should().BeTrue();
        response?.UpdatedNotificationSettings.Should().BeFalse();
        response?.UpdatedSecuritySettings.Should().BeFalse();
        response?.UpdatedProfileSettings.Should().BeFalse();
    }

    [Fact]
    public async Task Can_Update_Creator_Notification_Settings()
    {
        var request = new UpdateCreatorSettingsRequest
        {
            CreatorId = EXISTING_CREATOR_ID,
            NotificationNewMessage = new Include<bool>(false)
        };

        var response = await _OnboardingService.UpdateCreatorSettings(request);
        response.Should().NotBeNull();
        response.ResponseDetails.Should().NotBeNull();
        response?.ResponseDetails?.IsSuccess.Should().BeTrue();
        response?.UpdatedChatSettings.Should().BeFalse();
        response?.UpdatedNotificationSettings.Should().BeTrue();
        response?.UpdatedSecuritySettings.Should().BeFalse();
        response?.UpdatedProfileSettings.Should().BeFalse();
    }

    [Fact]
    public async Task Can_Update_Creator_Profile_Settings()
    {
        var request = new UpdateCreatorSettingsRequest
        {
            CreatorId = EXISTING_CREATOR_ID,
            DisplayName = new Include<string>("New Displayname")
        };

        var response = await _OnboardingService.UpdateCreatorSettings(request);
        response.Should().NotBeNull();
        response.ResponseDetails.Should().NotBeNull();
        response?.ResponseDetails?.IsSuccess.Should().BeTrue();
        response?.UpdatedChatSettings.Should().BeFalse();
        response?.UpdatedNotificationSettings.Should().BeFalse();
        response?.UpdatedSecuritySettings.Should().BeFalse();
        response?.UpdatedProfileSettings.Should().BeTrue();
    }

    [Fact]
    public async Task Can_Update_Creator_Security_Settings()
    {
        var request = new UpdateCreatorSettingsRequest
        {
            CreatorId = EXISTING_CREATOR_ID,
            ShowActivityStatus = new Include<bool>(true)
        };

        var response = await _OnboardingService.UpdateCreatorSettings(request);
        response.Should().NotBeNull();
        response.ResponseDetails.Should().NotBeNull();
        response?.ResponseDetails?.IsSuccess.Should().BeTrue();
        response?.UpdatedChatSettings.Should().BeFalse();
        response?.UpdatedNotificationSettings.Should().BeFalse();
        response?.UpdatedSecuritySettings.Should().BeTrue();
        response?.UpdatedProfileSettings.Should().BeFalse();
    }

    [Fact]
    public async Task Can_Update_Multiple_Creator_Settings()
    {
        var request = new UpdateCreatorSettingsRequest
        {
            CreatorId = EXISTING_CREATOR_ID,
            ShowActivityStatus = new Include<bool>(true),
            DisplayName = new Include<string>("Changing Profile")
        };

        var response = await _OnboardingService.UpdateCreatorSettings(request);
        response.Should().NotBeNull();
        response.ResponseDetails.Should().NotBeNull();
        response?.ResponseDetails?.IsSuccess.Should().BeTrue();
        response?.UpdatedChatSettings.Should().BeFalse();
        response?.UpdatedNotificationSettings.Should().BeFalse();
        response?.UpdatedSecuritySettings.Should().BeTrue();
        response?.UpdatedProfileSettings.Should().BeTrue();
    }
}