namespace onlysats.domain.Services.Request.Accounting
{
    public class InvoiceReceivedPaymentRequest : RequestBase
    {
        public string BtcPayServerAccountId { get; set; }
        public string InvoiceId { get; set; }
        public bool AfterExpiration { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public long ReceivedDate { get; set; }
        public string Value { get; set; }
        public string Fee { get; set; }
        public string PaymentStatus { get; set; }
        public string Destination { get; set; }
        
        public override bool IsValid()
        {
            return true;
        }
    }
}