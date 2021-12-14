using onlysats.domain.Entity;
using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of Assets and Asset Packages
/// </summary>
public interface IAssetRepository
{
    /// <summary>
    /// Retrieves an Asset from Id
    /// </summary>
    Task<Asset> GetAsset(int id);

    /// <summary>
    /// Retrieves an Asset Package from Id
    /// </summary>
    Task<AssetPackage> GetAssetPackage(int id);

    /// <summary>
    /// Retrieves all Assets within an AssetPackage
    /// </summary>
    Task<IEnumerable<Asset>> GetAssetsInPackage(int assetPackageId);

    /// <summary>
    /// Updates or Inserts an Asset
    /// </summary>
    Task<Asset> UpsertAsset(Asset asset);

    /// <summary>
    /// Updates or Inserts an Asset Package
    /// </summary>
    Task<AssetPackage> UpsertAssetPackage(AssetPackage assetPackage);
}

#region Implementation

public class AssetRepository : IAssetRepository
{
    private readonly ISqlRepository _Repository;

    public AssetRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }

    public Task<Asset> GetAsset(int id)
    {
        throw new NotImplementedException();
    }

    public Task<AssetPackage> GetAssetPackage(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Asset>> GetAssetsInPackage(int assetPackageId)
    {
        throw new NotImplementedException();
    }

    public Task<Asset> UpsertAsset(Asset asset)
    {
        throw new NotImplementedException();
    }

    public Task<AssetPackage> UpsertAssetPackage(AssetPackage assetPackage)
    {
        throw new NotImplementedException();
    }
}

#endregion