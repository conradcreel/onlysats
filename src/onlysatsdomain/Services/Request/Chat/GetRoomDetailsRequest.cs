using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Request.Chat
{
    public class GetRoomDetailsRequest : SynapseRequestBase
    {
        [JsonIgnore]
        public string RoomId { get; set; }

        public override string GenerateUrl()
        {
            return $"_synapse/admin/v1/rooms/{RoomId}";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(RoomId);
        }
    }
}