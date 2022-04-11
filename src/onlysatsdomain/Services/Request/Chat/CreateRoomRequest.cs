using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Request.Chat
{
    public class CreateRoomRequest : SynapseRequestBase
    {
        [JsonPropertyName("creation_content")]
        public CreationContent Settings { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("preset")]
        public string Preset { get; set; }

        [JsonPropertyName("room_alias_name")]
        public string Alias { get; set; }

        [JsonPropertyName("topic")]
        public string Topic { get; set; }

        [JsonIgnore]
        public int CreatorUserAccountId { get; set; }

        [JsonIgnore]
        public int PatronUserAccountId { get; set; }

        public override string GenerateUrl()
        {
            return "_matrix/client/v3/createRoom";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                    !string.IsNullOrWhiteSpace(Preset) &&
                    !string.IsNullOrWhiteSpace(Alias) &&
                    !string.IsNullOrWhiteSpace(Topic);
        }
    }

    public class CreationContent
    {
        [JsonPropertyName("m.federate")]
        public bool CanFederate { get; set; }
    }
}