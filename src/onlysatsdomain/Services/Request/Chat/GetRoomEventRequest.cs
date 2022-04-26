namespace onlysats.domain.Services.Request.Chat
{
    public class GetRoomEventRequest : SynapseRequestBase
    {
        public string EventId { get; set; }
        public string RoomId { get; set; }

        public override string GenerateUrl()
        {
            return $"_matrix/client/v3/rooms/{RoomId}/event/{EventId}";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(EventId);
        }
    }
}
