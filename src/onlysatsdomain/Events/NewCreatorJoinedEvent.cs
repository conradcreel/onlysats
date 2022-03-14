namespace onlysats.domain.Events
{

    /// <summary>
    /// Event that fires when a new Creator joins OnlySats
    /// </summary>
    public class NewCreatorJoinedEvent : EventBase
    {
        public int UserAccountId { get; set; }
        public int CreatorId { get; set; }
        public string Username { get; set; } = string.Empty;
        public override string Topic => nameof(NewCreatorJoinedEvent);
    }
}