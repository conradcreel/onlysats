namespace onlysats.domain.Services.Request
{
    public abstract class SynapseRequestBase : RequestBase
    {
        public string AccessToken { get; set; }
        public abstract string GenerateUrl();
    }
}