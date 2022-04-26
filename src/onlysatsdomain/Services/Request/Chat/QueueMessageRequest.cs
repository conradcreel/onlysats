namespace onlysats.domain.Services.Request.Chat
{
    public class QueueMessageRequest : RequestBase
    {
        public int SenderUserId { get; set; }
        public string SynapseAccessToken { get; set; }
        public string RoomId { get; set; }
        public string MessageContent { get; set; }

        /// <summary>
        /// Teaser description that will display before purchase
        /// </summary>
        public string Description { get; set; }
        public bool PaymentRequired { get; set; }
        public int? AssetPackageId { get; set; } // if packageId is null then MessageContent is constructed server-side

        /// <summary>
        /// How many Satoshis does this message cost?
        /// </summary>
        public int? CostInSatoshis { get; set; }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(RoomId);
        }
    }
}