namespace onlysats.domain.Events
{

    /// <summary>
    ///
    /// </summary>
    public class WalletUpdatedEvent : EventBase
    {
        public int WalletId { get; set; }
        public int UserAccountId { get; set; }
        public string Nickname { get; set; }
        public string BtcPaymentProcessorId { get; set; }
        public override string Topic => nameof(WalletUpdatedEvent);
    }
}