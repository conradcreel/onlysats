using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Request
{
    public abstract class SynapseRequestBase : RequestBase
    {
        [JsonIgnore]
        public string AccessToken { get; set; }
        public abstract string GenerateUrl();
    }
}