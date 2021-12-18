using onlysats.domain.Models;

namespace onlysats.domain.Services.Response.Onboarding;

public class LoadCreatorProfileResponse : ResponseBase
{
    public CreatorModel Creator { get; set; }
}