using System.Text.Json.Serialization;

namespace onlysats.domain.Models.BtcPayServer
{
    public class InvoicePaymentMethodModel
    {
        [JsonPropertyName("destination")]
        public string Destination { get; set; } // BOLT11 for LN
    }
}
