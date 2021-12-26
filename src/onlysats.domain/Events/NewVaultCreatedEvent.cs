namespace onlysats.domain.Events;

public class NewVaultCreatedEvent : EventBase
{
    public int CreatorId { get; set; }
    public int VaultId { get; set; }
    public string VaultName { get; set; } = string.Empty;
    public override string Topic => nameof(NewVaultCreatedEvent);
}