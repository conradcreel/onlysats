namespace onlysatsweb.Models.Chat
{
    public class SendPaidMessageModel
    {
        public string RoomId { get; set; }
        public int AssetPackageId { get; set; }
        public int CostInSatoshis { get; set; }
        public string Description { get; set; }
    }
}
