using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Request.Chat
{
    public class AddUserToRoomRequest : SynapseRequestBase
    {

        [JsonPropertyName("user_id")]
        public string SynapseUserId { get; set; }

        [JsonIgnore]
        public string RoomId { get; set; }

        public override string GenerateUrl()
        {
            return $"_synapse/admin/v1/join/{RoomId}";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(SynapseUserId) &&
                    !string.IsNullOrWhiteSpace(RoomId);
        }
    }
}