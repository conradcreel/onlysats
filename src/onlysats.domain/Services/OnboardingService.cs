using onlysats.domain.Services.Repositories;
using onlysats.domain.Services.Request.Onboarding;
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
    ///
    /// </summary>
    Task<SetupCreatorResponse> SetupCreator(SetupCreatorRequest request);

    /// <summary>
    ///
    /// </summary>
    Task<SetupPatronResponse> SetupPatron(SetupPatronRequest request);

    /// <summary>
    ///
    /// </summary>
    Task<UpdateCreatorSettingsResponse> UpdateCreatorSettings(UpdateCreatorSettingsRequest request);

    /// <summary>
    ///
    /// </summary>
    Task<UpdatePatronSettingsResponse> UpdatePatronSettings(UpdatePatronSettingsRequest request);
}

#region Implementation

public class OnboardingService : IOnboardingService
{
    private readonly IUserAccountRepository _UserAccountRepository;
    private readonly ICreatorRepository _CreatorRepository;
    private readonly IPatronRepository _PatronRepository;

    public OnboardingService(IUserAccountRepository userAccountRepository,
                            ICreatorRepository creatorRepository,
                            IPatronRepository patronRepository
                            )
    {
        _UserAccountRepository = userAccountRepository;
        _CreatorRepository = creatorRepository;
        _PatronRepository = patronRepository;
    }

    public Task<SetupCreatorResponse> SetupCreator(SetupCreatorRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<SetupPatronResponse> SetupPatron(SetupPatronRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateCreatorSettingsResponse> UpdateCreatorSettings(UpdateCreatorSettingsRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<UpdatePatronSettingsResponse> UpdatePatronSettings(UpdatePatronSettingsRequest request)
    {
        throw new NotImplementedException();
    }
}

#endregion
