using onlysats.domain.Entity;
using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of Vaults
/// </summary>
public interface IVaultRepository
{

    /// <summary>
    ///
    /// </sumamry>
    Task<Vault> GetVault(int vaultId);

    /// <summary>
    ///
    /// </sumamry>
    Task<IEnumerable<Vault>> GetVaults(int creatorId, int top = 10, int skip = 0);

    /// <summary>
    ///
    /// </sumamry>
    Task<Vault> UpsertVault(Vault vault);
}

#region Implementation

public class VaultRepository : IVaultRepository
{
    private readonly ISqlRepository _Repository;

    public VaultRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }

    public Task<Vault> GetVault(int vaultId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Vault>> GetVaults(int creatorId, int top = 10, int skip = 0)
    {
        throw new NotImplementedException();
    }

    public Task<Vault> UpsertVault(Vault vault)
    {
        throw new NotImplementedException();
    }
}

#endregion