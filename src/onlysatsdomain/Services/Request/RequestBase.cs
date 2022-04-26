using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Request
{

    public abstract class RequestBase
    {
        [JsonIgnore]
        public AuthenticatedUserContext UserContext { get; set; }
        [JsonIgnore]
        public bool AdminRequest { get; set; }
        public abstract bool IsValid();
    }
}