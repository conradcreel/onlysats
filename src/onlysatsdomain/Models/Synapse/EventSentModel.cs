using System.Text.Json.Serialization;

namespace onlysats.domain.Models.Synapse
{
    public class EventSentModel
    {
        [JsonPropertyName("event_id")]
        public string EventId { get; set; }
    }
}