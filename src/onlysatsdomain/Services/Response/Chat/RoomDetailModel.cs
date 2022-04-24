using System.Text.Json.Serialization;

namespace onlysatsdomain.Services.Response.Chat
{
    public class RoomDetailModel
    {
        [JsonPropertyName("room_id")]
        public string RoomId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("canonical_alias")]
        public string Alias { get; set; }

        [JsonPropertyName("joined_members")]
        public int JoinedMembers { get; set; }

        [JsonPropertyName("joined_local_members")]
        public int JoinedLocalMembers { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("creator")]
        public string Creator { get; set; }

        [JsonPropertyName("federatable")]
        public int Federatable { get; set; }

        [JsonPropertyName("public")]
        public int Public { get; set; }

        [JsonPropertyName("join_rules")]
        public string JoinRules { get; set; }

        [JsonPropertyName("guest_access")]
        public string GuessAccess { get; set; }

        [JsonPropertyName("history_visibility")]
        public string HistoryVisibility { get; set; }

        [JsonPropertyName("state_events")]
        public int StateEvents { get; set; }
    }
}
