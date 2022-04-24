using onlysatsdomain.Services.Response.Chat;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Response.Chat
{
    public class GetRoomListResponse : ResponseBase
    {
        [JsonPropertyName("joined_rooms")]
        public List<string> JoinedRooms { get; set; }

        #region for Admin request to all /rooms

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("total_rooms")]
        public int TotalRooms { get; set; }

        [JsonPropertyName("rooms")]
        public List<RoomDetailModel> Rooms { get; set; }

        #endregion 
    }
}