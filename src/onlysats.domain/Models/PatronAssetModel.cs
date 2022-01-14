using onlysats.domain.Entity;

namespace onlysats.domain.Models;

public class PatronAssetModel
{

    public PatronAssetModel(PatronAsset patronAsset, Asset asset)
    {
        PatronId = patronAsset.PatronId;
        CreatorId = patronAsset.CreatorId;
        AssetId = asset.Id;
        UniqueAssetUri = patronAsset.UniqueAssetUri;
        DateAcquired = patronAsset.DateAcquired;
        DisplayName = asset.DisplayName;
        Description = asset.Description ?? string.Empty;
        Type = asset.Type.ToString();
    }

    public int PatronId { get; set; }
    public int CreatorId { get; set; }
    public int AssetId { get; set; }
    public string? UniqueAssetUri { get; set; }
    public DateTime DateAcquired { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}