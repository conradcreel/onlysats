using onlysats.domain.Models;

namespace onlysats.domain.Services.Response.Onboarding;

public class LoadPatronProfileResponse : ResponseBase
{
    public PatronModel? Patron { get; set; }
}