namespace onlysats.domain.Events;

/// <summary>
///
/// </summary>
public class WalletUpdatedEvent : EventBase
{
    public override string Topic => nameof(WalletUpdatedEvent);
}