using System;

namespace onlysats.domain.Models.BtcPayServer
{
    public class CreateLightningPaymentRequest
    {
        public string Description { get; set; }
        public string Id { get { return Guid.NewGuid().ToString(); } }
        public int Satoshis { get; set; }

        public int ExpiryMinutes { get; set; }
    }
}
