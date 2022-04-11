using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Request.Chat
{
    public class ReleaseMessageRequest : SynapseRequestBase
    {
        [JsonIgnore]
        public string RoomId { get; set; }

        [JsonIgnore]
        public string IdempotencyKey
        {
            get
            {
                return "2"; // TODO: Maybe hash this object?
            }
        }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("msgtype")]
        public string Type { get; set; } = "m.text";

        [JsonPropertyName("format")]
        public string Format { get; set; } = "org.matrix.custom.html";

        [JsonPropertyName("formatted_body")]
        public string FormattedBody { get; set; }

        public override string GenerateUrl()
        {
            return $"_matrix/client/v3/rooms/{RoomId}/send/m.room.message/{IdempotencyKey}";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Body) &&
                    !string.IsNullOrWhiteSpace(FormattedBody);
        }
    }
}