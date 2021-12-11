namespace onlysats.domain.Models;

/// <summary>TODO</summary>
public class Asset
{
    public int Id { get; set; }
    public EAssetType Type { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string LocalHash { get; set; } = string.Empty;
    public string RemoteLocation { get; set; } = string.Empty;
    public EAssetStatus Status { get; set; }
}
