using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Request.Chat
{
    public class GetRoomEventsRequest : SynapseRequestBase
    {
        [JsonIgnore]
        public string RoomId { get; set; }

        [JsonIgnore]
        public uint Limit { get; set; } = 1;//10;

        [JsonIgnore]
        public string From { get; set; }

        [JsonIgnore]
        public bool FetchNew { get; set; }

        public override string GenerateUrl()
        {
            // TODO: Might want to add an option to this request 
            // object which informs how to construct the filter. 
            // For right now, I think we only want the room messages so
            // it's going to be hard coded 
            var filter = new
            {
                types = new List<string>
                {
                    "m.room.message"
                }
            };
            string dir = "b";
            if (FetchNew)
            {
                dir = "f";
            }

            var url = $"_matrix/client/v3/rooms/{RoomId}/messages?filter={JsonSerializer.Serialize(filter)}&limit={Limit}&dir={dir}";

            if (!string.IsNullOrWhiteSpace(From))
            {
                url = $"{url}&from={From}";
            }

            return url;
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(RoomId);
        }
    }
}