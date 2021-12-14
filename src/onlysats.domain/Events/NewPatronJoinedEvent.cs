namespace onlysats.domain.Events;

/// <summary>
/// Event that fires when a new Patron joins OnlySats
/// </summary>
public class NewPatronJoinedEvent : EventBase
{
    public override string Topic => nameof(NewPatronJoinedEvent);
}