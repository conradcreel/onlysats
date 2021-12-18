using onlysats.domain.Constants;
using onlysats.domain.Entity;
using onlysats.domain.Enums;
using onlysats.domain.Events;
using onlysats.domain.Models;
using onlysats.domain.Services.Repositories;
using onlysats.domain.Services.Request.Onboarding;
using onlysats.domain.Services.Response;
using onlysats.domain.Services.Response.Onboarding;

namespace onlysats.domain.Services;

/// <summary>
/// Handles all account and settings setup (except wallet import).
/// While wallet/BTCPay Server setup could be considered part of 
/// onboarding, it's purposely encapsulated within the Accounting 
/// service for extensibility without tightly coupling user
/// setup with payment setup.
/// </summary>
public interface IOnboardingService
{
    /// <summary>
    /// Sets up a new Creator with the supplied personal information with default settings
    /// </summary>
    Task<SetupCreatorResponse> SetupCreator(SetupCreatorRequest request);

    /// <summary>
    /// Sets up a new Patron with the supplied personal information with default settings
    /// </summary>
    Task<SetupPatronResponse> SetupPatron(SetupPatronRequest request);

    /// <summary>
    /// Updates a Creator's Settings/Preferences 
    /// </summary>
    Task<UpdateCreatorSettingsResponse> UpdateCreatorSettings(UpdateCreatorSettingsRequest request);

    /// <summary>
    /// Updates a Patron's Settings/Preferences
    /// </summary>
    Task<UpdatePatronSettingsResponse> UpdatePatronSettings(UpdatePatronSettingsRequest request);

    /// <summary>
    /// Loads the details of a Creator
    /// </summary>
    Task<LoadCreatorProfileResponse> LoadCreatorProfile(LoadCreatorProfileRequest request);

    /// <summary>
    /// Loads the details of a Patron
    /// </summary>
    Task<LoadPatronProfileResponse> LoadPatronProfile(LoadPatronProfileRequest request);
}

#region Implementation

public class OnboardingService : IOnboardingService
{
    private readonly IUserAccountRepository _UserAccountRepository;
    private readonly ICreatorRepository _CreatorRepository;
    private readonly IPatronRepository _PatronRepository;
    private readonly IMessagePublisher _MessagePublisher;

    public OnboardingService(IUserAccountRepository userAccountRepository,
                            ICreatorRepository creatorRepository,
                            IPatronRepository patronRepository,
                            IMessagePublisher messagePublisher
                            )
    {
        _UserAccountRepository = userAccountRepository;
        _CreatorRepository = creatorRepository;
        _PatronRepository = patronRepository;
        _MessagePublisher = messagePublisher;
    }

    public async Task<LoadCreatorProfileResponse> LoadCreatorProfile(LoadCreatorProfileRequest request)
    {
        if (request == null || !request.IsValid())
        {
            return new LoadCreatorProfileResponse()
                        .BadRequest(CErrorMessage.LOAD_CREATOR_BAD_REQUEST);
        }

        var creatorDetails = await _CreatorRepository.GetCreatorDetail(request.CreatorId);

        if (creatorDetails == null)
        {
            return new LoadCreatorProfileResponse().NotFound();
        }

        return new LoadCreatorProfileResponse
        {
            Creator = creatorDetails
        }.OK();
    }

    public Task<LoadPatronProfileResponse> LoadPatronProfile(LoadPatronProfileRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<SetupCreatorResponse> SetupCreator(SetupCreatorRequest request)
    {
        if (request == null || !request.IsValid())
        {
            return new SetupCreatorResponse()
                        .BadRequest(CErrorMessage.SETUP_CREATOR_BAD_REQUEST);
        }

        var userAccount = await _UserAccountRepository.UpsertUserAccount(new UserAccount
        {
            Username = request.Username,
            UserId = request.UserId,
            IdpSource = request.IdpSource,
            Email = request.Email,
            Role = EUserRole.CREATOR
        });

        if (userAccount == null)
        {
            return new SetupCreatorResponse()
                        .ServerError(CErrorMessage.SETUP_CREATOR_FAIL_USER_ACCOUNT);
        }

        var creator = await _CreatorRepository.UpsertCreator(new Creator
        {
            UserAccountId = userAccount.Id
        });

        if (creator == null)
        {
            return new SetupCreatorResponse()
                        .ServerError(CErrorMessage.SETUP_CREATOR_FAIL);
        }

        var domainEvent = new NewCreatorJoinedEvent
        {
            CreatorId = creator.Id,
            Username = userAccount.Username
        };

        await _MessagePublisher.PublishEvent(domainEvent.Topic, domainEvent);

        return new SetupCreatorResponse
        {
            UserAccountId = userAccount.Id,
            CreatorId = creator.Id
        }.OK();
    }

    public async Task<SetupPatronResponse> SetupPatron(SetupPatronRequest request)
    {
        if (request == null || !request.IsValid())
        {
            return new SetupPatronResponse()
                        .BadRequest(CErrorMessage.SETUP_PATRON_BAD_REQUEST);
        }

        // TODO: Create UserAccount 

        // TODO: Create Patron

        var domainEvent = new NewPatronJoinedEvent
        {
            // TODO
        };

        await _MessagePublisher.PublishEvent(domainEvent.Topic, domainEvent);

        throw new NotImplementedException();
    }

    public async Task<UpdateCreatorSettingsResponse> UpdateCreatorSettings(UpdateCreatorSettingsRequest request)
    {
        if (request == null || !request.IsValid())
        {
            return new UpdateCreatorSettingsResponse()
                        .BadRequest(CErrorMessage.UPDATE_CREATOR_SETTINGS_BAD_REQUEST);
        }

        if (request.ChatHideOutgoingMassMessages != null ||
            request.ChatShowWelcomeMessage != null)
        {
            // TODO: Update Chat Settings
        }

        if (request.NotificationUsePush != null ||
            request.NotificationUseEmail != null ||
            request.NotificationNewMessage != null ||
            request.NotificationNewSubscriber != null ||
            request.NotificationNewTip != null ||
            request.NotificationNewPurchase != null)
        {
            // TODO: Update Notification Settings
        }

        if (request.DisplayName != null ||
            request.CoverPhotoUri != null ||
            request.ProfilePhotoUri != null ||
            request.SubscriptionPricePerMonth != null ||
            request.AboutHtml != null ||
            request.AmazonWishList != null)
        {
            // TODO: Update Profile Settings
        }

        if (request.ShowActivityStatus != null ||
            request.FullyPrivateProfile != null ||
            request.ShowPatronCount != null ||
            request.ShowMediaCount != null ||
            request.WatermarkPhotos != null ||
            request.WatermarkVideos != null)
        {
            // TODO: Update Security Settings
        }

        return new UpdateCreatorSettingsResponse().OK();
    }

    public async Task<UpdatePatronSettingsResponse> UpdatePatronSettings(UpdatePatronSettingsRequest request)
    {
        if (request == null || !request.IsValid())
        {
            return new UpdatePatronSettingsResponse()
                        .BadRequest(CErrorMessage.UPDATE_PATRON_SETTINGS_BAD_REQUEST);
        }

        throw new NotImplementedException();
    }
}

#endregion
