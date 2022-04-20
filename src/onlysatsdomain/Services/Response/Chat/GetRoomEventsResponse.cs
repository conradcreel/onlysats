using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Response.Chat
{
    public class GetRoomEventsResponse : ResponseBase
    {
        [JsonPropertyName("chunk")]
        public List<RoomEvent> RoomEvents { get; set; }

        [JsonPropertyName("start")]
        public string Start { get; set; }

        [JsonPropertyName("end")]
        public string End { get; set; }
    }

    public class RoomEvent
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

    public class RoomMessage
    {
        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("msgtype")]
        public string MessageType { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("formatted_body")]
        public string FormattedBody { get; set; }
    }
}