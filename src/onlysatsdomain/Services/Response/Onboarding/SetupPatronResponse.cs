namespace onlysats.domain.Services.Response.Onboarding
{
    public class SetupPatronResponse : ResponseBase
    {
        public int UserAccountId { get; set; }
        public int PatronId { get; set; }
    }
}