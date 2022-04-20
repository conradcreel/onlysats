using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Response.Chat
{
    public class SynapseLoginResponse : ResponseBase
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("home_server")]
        public string HomeServer { get; set; }

        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }
    }
}