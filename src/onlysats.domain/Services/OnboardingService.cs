using onlysats.domain.Services.Request.Onboarding;
using onlysats.domain.Services.Response.Onboarding;

namespace onlysats.domain.Services;

/// <summary>
/// Handles all account and settings setup (except wallet import)
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
    private readonly BtcPayServerProxy _BtcPayProxy;

    public OnboardingService(BtcPayServerProxy btcPayProxy)
    {
        _BtcPayProxy = btcPayProxy;
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
