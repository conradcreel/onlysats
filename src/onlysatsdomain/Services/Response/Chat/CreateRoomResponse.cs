using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Response.Chat
{
    public class CreateRoomResponse : ResponseBase
    {
        [JsonPropertyName("room_id")]
        public string RoomId { get; set; }

        [JsonPropertyName("room_alias")]
        public string RoomAlias { get; set; }
    }
}