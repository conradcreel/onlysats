using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Request.Chat
{
    public class SynapseLoginRequest : SynapseRequestBase
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "m.login.password";

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        public override string GenerateUrl()
        {
            return "_matrix/client/v3/login";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(User) &&
                    !string.IsNullOrWhiteSpace(Password);
        }
    }
}