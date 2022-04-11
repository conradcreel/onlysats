namespace onlysats.domain.Services.Request.Accounting
{
    public class InvoiceProcessingRequest : RequestBase
    {
        public string BtcPayServerAccountId { get; set; }
        public string InvoiceId { get; set; }
        public bool OverPaid { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}