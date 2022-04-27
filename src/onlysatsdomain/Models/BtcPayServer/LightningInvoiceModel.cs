using System.Text.Json.Serialization;

namespace onlysats.domain.Models.BtcPayServer
{
    public class LightningInvoiceModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("destination")]
        public string BOLT11 { get; set; }

        [JsonPropertyName("amount")]
        public string Satoshis { get; set; }
    }
}
