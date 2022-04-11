namespace onlysats.domain.Services.Response.Chat
{
    public class QueueMessageResponse : ResponseBase
    {
        public int QueuedMessageId { get; set; }
        public string InvoiceId { get; set; }
        public string BOLT11 { get; set; }
    }
}