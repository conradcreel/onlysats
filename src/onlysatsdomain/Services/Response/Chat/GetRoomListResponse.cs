using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Response.Chat
{
    public class GetRoomListResponse : ResponseBase
    {
        [JsonPropertyName("joined_rooms")]
        public List<string> JoinedRooms { get; set; }
    }
}