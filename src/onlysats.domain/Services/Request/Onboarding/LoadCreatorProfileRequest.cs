namespace onlysats.domain.Services.Request.Onboarding;

public class LoadCreatorProfileRequest : RequestBase
{
    public int CreatorId { get; set; }

    public override bool IsValid()
    {
        return true;
    }
}