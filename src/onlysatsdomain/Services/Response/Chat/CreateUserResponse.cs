using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Response.Chat
{
    public class CreateUserResponse : ResponseBase
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("is_guest")]
        public bool IsGuest { get; set; }

        [JsonPropertyName("admin")]
        public bool Admin { get; set; }

        [JsonPropertyName("creation_ts")]
        public long CreationTimestamp { get; set; }

        [JsonPropertyName("deactivated")]
        public bool Deactivated { get; set; }

        [JsonPropertyName("shadow_banned")]
        public bool ShadowBanned { get; set; }

        [JsonPropertyName("displayname")]
        public string DisplayName { get; set; }

        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; }
    }
}