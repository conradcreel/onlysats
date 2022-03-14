namespace onlysats.domain.Services.Request.Onboarding
{
    public class LoadPatronProfileRequest : RequestBase
    {
        public int PatronId { get; set; }

        public override bool IsValid()
        {
            return PatronId > 0;
        }
    }
}