namespace onlysats.domain.Events
{

    public class AssetPackagePublishedEvent : EventBase
    {
        public int AssetPackageId { get; set; }
        public int CreatorId { get; set; }
        public string AssetPackageName { get; set; } = string.Empty;
        public int VaultId { get; set; }
        public override string Topic => nameof(AssetPackagePublishedEvent);
    }
}