using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using onlysats.domain.Entity;
using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories
{

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
        /// Returns the number of Vaults this Creator has access to
        /// </summary>
        Task<int> GetVaultCount(int creatorId);

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

        public Task<int> GetVaultCount(int creatorId)
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
}