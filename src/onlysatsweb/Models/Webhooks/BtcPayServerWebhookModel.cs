using System.Text.Json.Serialization;

namespace onlysats.web.Models.Webhooks
{
    public class BtcPayServerWebhookModel
    {
        [JsonPropertyName("deliveryId")]
        public string DeliveryId { get; set; }

        [JsonPropertyName("webhookId")]
        public string WebhookId { get; set; }

        [JsonPropertyName("originalDeliveryId")]
        public string OriginalDeliveryId { get; set; }

        [JsonPropertyName("isRedelivery")]
        public bool IsRedelivery { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("storeId")]
        public string StoreId { get; set; }

        [JsonPropertyName("invoiceId")]
        public string InvoiceId { get; set; }

        #region InvoiceExpired

        [JsonPropertyName("partiallyPaid")]
        public bool PartiallyPaid { get; set; }

        #endregion

        #region InvoiceReceivedPayment | InvoicePaymentSettled

        [JsonPropertyName("afterExpiration")]
        public bool AfterExpiration { get; set; }

        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("payment")]
        public BtcPayServerPaymentData Payment { get; set; }

        #endregion

        #region InvoiceProcessing | InvoiceSettled

        [JsonPropertyName("overPaid")]
        public bool OverPaid { get; set; }

        #endregion

        #region InvoiceInvalid | InvoiceSettled

        [JsonPropertyName("manuallyMarked")]
        public bool ManuallyMarked { get; set; }

        #endregion 
    }

    public class BtcPayServerPaymentData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("receivedDate")]
        public long ReceivedDate { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("fee")]
        public string Fee { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("destination")]
        public string Destination { get; set; }
    }
}