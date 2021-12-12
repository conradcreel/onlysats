using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of Vaults
/// </summary>
public interface IVaultRepository
{

}

#region Implementation

public class VaultRepository : IVaultRepository 
{
    private readonly ISqlRepository _Repository;

    public VaultRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }
}

#endregion 