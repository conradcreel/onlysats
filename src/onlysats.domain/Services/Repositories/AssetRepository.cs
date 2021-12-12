using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
///
/// </summary>
public interface IAssetRepository
{

}

#region Implementation

public class AssetRepository : IAssetRepository
{
    private readonly SqlRepository _Repository;

    public AssetRepository(OnlySatsConfiguration config)
    {
        _Repository = new SqlRepository(config.SqlConnectionString);
    }
}

#endregion