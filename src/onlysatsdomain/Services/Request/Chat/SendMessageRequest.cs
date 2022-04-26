using System;
using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Request.Chat
{
    public class SendMessageRequest : SynapseRequestBase
    {
        [JsonIgnore]
        public string RoomId { get; set; }

        [JsonIgnore]
        public string IdempotencyKey
        {
            get
            {
                //return HashService.HashSHA256(FormattedBody);

                // The above was a decent idea, but won't work because 
                // it won't send the same message. Like if someone replied with "okay"
                // it would send the first time but not subsequent messages
                return Guid.NewGuid().ToString();
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