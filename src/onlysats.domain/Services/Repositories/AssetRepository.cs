using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of Assets and Asset Packages
/// </summary>
public interface IAssetRepository
{

}

#region Implementation

public class AssetRepository : IAssetRepository
{
    private readonly ISqlRepository _Repository;

    public AssetRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }
}

#endregion