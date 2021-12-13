namespace onlysats.domain.Events;

public class NewCreatorJoinedEvent : EventBase
{
    public override string Topic => nameof(NewCreatorJoinedEvent);
}