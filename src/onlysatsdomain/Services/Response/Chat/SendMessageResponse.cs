using System.Text.Json.Serialization;

namespace onlysats.domain.Services.Response.Chat
{
    public class SendMessageResponse : ResponseBase
    {
        [JsonPropertyName("event_id")]
        public string EventId { get; set; }
    }
}