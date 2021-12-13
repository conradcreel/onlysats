namespace onlysats.domain.Services.Request.Onboarding;

public class SetupCreatorRequest : RequestBase
{
    public string IdpSource { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;

    private string[] AllowedIdentityProviders = new string[]
    {
        "Auth0"
    };

    public override bool IsValid()
    {
        return AllowedIdentityProviders.Contains(IdpSource) &&
                !string.IsNullOrWhiteSpace(UserId) &&
                !string.IsNullOrWhiteSpace(Email) &&
                !string.IsNullOrWhiteSpace(Username);
    }
}