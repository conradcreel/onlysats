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
    Task<Asset?> GetAsset(int id);

    /// <summary>
    /// Retrieve a list of Assets from filters
    /// </summary>
    Task<IEnumerable<Asset>?> GetAssets(int? creatorId = null, int? vaultId = null, List<int>? assetIds = null, int top = 10, int skip = 0);

    /// <summary>
    /// Gets the total number of assets matching the filters
    /// </summary>
    Task<int> GetAssetCount(int? creatorId = null, int? vaultId = null, List<int>? assetIds = null);

    /// <summary>
    /// Retrieves Asset Packages from filters
    /// </summary>
    Task<IEnumerable<AssetPackage>?> GetAssetPackages(int? creatorId = null, int? vaultId = null, List<int>? assetPackageIds = null, int top = 10, int skip = 0);

    /// <summary>
    /// Gets the total number of assetPackages matching the filters
    /// </summary>
    Task<int> GetAssetPackageCount(int? creatorId = null, int? vaultId = null, List<int>? assetPackageIds = null);

    /// <summary>
    /// Retrieves an Asset Package from Id
    /// </summary>
    Task<AssetPackage?> GetAssetPackage(int id);

    /// <summary>
    /// Retrieves all Assets within an AssetPackage
    /// </summary>
    Task<IEnumerable<Asset>?> GetAssetsInPackage(int assetPackageId, int top = 10, int skip = 0);

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

    public Task<Asset?> GetAsset(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetAssetCount(int? creatorId = null, int? vaultId = null, List<int>? assetIds = null)
    {
        throw new NotImplementedException();
    }

    public Task<AssetPackage?> GetAssetPackage(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetAssetPackageCount(int? creatorId = null, int? vaultId = null, List<int>? assetPackageIds = null)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AssetPackage>?> GetAssetPackages(int? creatorId = null, int? vaultId = null, List<int>? assetPackageIds = null, int top = 10, int skip = 0)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Asset>?> GetAssets(int? creatorId = null, int? vaultId = null, List<int>? assetIds = null, int top = 10, int skip = 0)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Asset>?> GetAssetsInPackage(int assetPackageId, int top = 10, int skip = 0)
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