using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Response.Chat
{
    public class GetRoomEventResponse : ResponseBase
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("room_id")]
        public string RoomId { get; set; }

        [JsonPropertyName("sender")]
        public string Sender { get; set; }

        [JsonPropertyName("content")]
        public RoomMessage Message { get; set; }

        [JsonPropertyName("origin_server_ts")]
        public long OriginServerTimestamp { get; set; }

        [JsonPropertyName("event_id")]
        public string EventId { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("age")]
        public long Age { get; set; }
    }
}
