using onlysats.domain.Enums;

namespace onlysats.domain.Entity
{
    public class QueuedMessage : BaseEntity
    {
        public int CreatorId { get; set; }
        public int PatronId { get; set; }
        public string InvoiceId { get; set; }
        public string BOLT11 { get; set; }
        public string RoomId { get; set; }
        public string MessageContent { get; set; }
    }
}