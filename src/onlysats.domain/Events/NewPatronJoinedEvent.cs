namespace onlysats.domain.Events;

/// <summary>
/// Event that fires when a new Patron joins OnlySats
/// </summary>
public class NewPatronJoinedEvent : EventBase
{
    public int UserAccountId { get; set; }
    public int PatronId { get; set; }
    public string Username { get; set; } = string.Empty;
    public override string Topic => nameof(NewPatronJoinedEvent);
}