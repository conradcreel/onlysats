using System.Threading.Tasks;
using onlysats.domain.Entity;
using onlysats.domain.Enums;
using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories
{

    /// <summary>
    /// Encapsulates persistence of User Accounts
    /// </summary>
    public interface IUserAccountRepository
    {
        Task<UserAccount> GetUserAccount(int id);
        Task<UserAccount> GetUserAccount(string userName, string passwordHash);
        Task<UserAccount> UpsertUserAccount(UserAccount userAccount);
    }

    #region Implementation

    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly ISqlRepository _Repository;

        public UserAccountRepository(ISqlRepository sqlRepository)
        {
            _Repository = sqlRepository;
        }

        public async Task<UserAccount> GetUserAccount(int id)
        {
            string sql = $@"";

            return await _Repository.SelectSingle<UserAccount>(sql);
        }

        public async Task<UserAccount> GetUserAccount(string userName, string passwordHash)
        {
            await Task.Delay(10);

            if (userName != "simp1")
            {
                return null;
            }
            const string defaultPassword = "490763548a9745b09a907dd8ad84d3ee";

            return new UserAccount
            {
                Id = 1337,
                Username = "simp1",
                ChatPassword = defaultPassword,
                Role = EUserRole.PATRON
            };
        }

        public async Task<UserAccount> UpsertUserAccount(UserAccount userAccount)
        {
            string sql = $@"";

            return await _Repository.Upsert<UserAccount>(sql);
        }
    }

    #endregion
}