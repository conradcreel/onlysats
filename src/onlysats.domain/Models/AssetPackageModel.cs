using onlysats.domain.Entity;

namespace onlysats.domain.Models;

public class AssetPackageModel
{
    public AssetPackageModel(AssetPackage assetPackageEntity, string creatorUserName)
    {
        VaultId = assetPackageEntity.VaultId;
        Creator = creatorUserName;
        AssetPackageId = assetPackageEntity.Id;
        DisplayName = assetPackageEntity.DisplayName;
        Description = assetPackageEntity.Description;
        CoverPhotoUri = assetPackageEntity.CoverPhotoUri;
        BuyNowPrice = assetPackageEntity.BuyNowPrice;
        if (assetPackageEntity.AssetIds != null && assetPackageEntity.AssetIds.Any())
        {
            NumAssets = assetPackageEntity.AssetIds.Count;
        }
    }

    public AssetPackageModel(AssetPackage assetPackageEntity, string creatorUserName, IEnumerable<Asset> assetEntities) : this(assetPackageEntity, creatorUserName)
    {
        if (assetEntities.Any())
        {
            Assets = new List<AssetModel>();
            foreach (var assetEntity in assetEntities)
            {
                Assets.Add(new AssetModel(assetEntity, creatorUserName));
            }
        }
    }

    public int VaultId { get; }
    public string Creator { get; } // This is the container name in Blob Storage if assets are included
    public int AssetPackageId { get; }
    public string DisplayName { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public string? CoverPhotoUri { get; }
    public double BuyNowPrice { get; }
    public int NumAssets { get; }

    public List<AssetModel>? Assets { get; }
}