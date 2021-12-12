using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
///
/// </summary>
public interface IVaultRepository
{

}

#region Implementation

public class VaultRepository : IVaultRepository 
{
    private readonly SqlRepository _Repository;

    public VaultRepository(OnlySatsConfiguration config)
    {
        _Repository = new SqlRepository(config.SqlConnectionString);
    }
}

#endregion 