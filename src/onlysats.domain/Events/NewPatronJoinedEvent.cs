namespace onlysats.domain.Events;

public class NewPatronJoinedEvent : EventBase
{
    public override string Topic => nameof(NewPatronJoinedEvent);
}