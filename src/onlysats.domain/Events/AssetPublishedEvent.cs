namespace onlysats.domain.Events;

public class AssetPublishedEvent : EventBase
{
    public int AssetId { get; set; }
    public int CreatorId { get; set; }
    public string AssetName { get; set; } = string.Empty;
    public int VaultId { get; set; }
    public override string Topic => nameof(AssetPublishedEvent);
}