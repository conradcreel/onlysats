using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Request.Chat
{
    public class CreateUserRequest : SynapseRequestBase
    {
        [JsonIgnore]
        public string Username { get; set; }

        [JsonIgnore]
        public string Homeserver { get; set; } = "onlysats.matrix";

        [JsonPropertyName("displayname")]
        public string DisplayName { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        public override string GenerateUrl()
        {
            return $"_synapse/admin/v2/users/@{Username}:{Homeserver}";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(DisplayName) &&
                    !string.IsNullOrWhiteSpace(Password);
        }
    }
}