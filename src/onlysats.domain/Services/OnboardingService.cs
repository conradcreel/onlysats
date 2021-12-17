using onlysats.domain.Constants;
using onlysats.domain.Entity;
using onlysats.domain.Enums;
using onlysats.domain.Events;
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
}

#region Implementation

public class OnboardingService : IOnboardingService
{
    private readonly IUserAccountRepository _UserAccountRepository;
    private readonly ICreatorRepository _CreatorRepository;
    private readonly IPatronRepository _PatronRepository;
    private readonly MessagePublisherProxy _MessagePublisher;

    public OnboardingService(IUserAccountRepository userAccountRepository,
                            ICreatorRepository creatorRepository,
                            IPatronRepository patronRepository,
                            MessagePublisherProxy messagePublisher
                            )
    {
        _UserAccountRepository = userAccountRepository;
        _CreatorRepository = creatorRepository;
        _PatronRepository = patronRepository;
        _MessagePublisher = messagePublisher;
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
            UserAccountId = userAccount.Id,
            /* TODO: Others */
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

        throw new NotImplementedException();
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
