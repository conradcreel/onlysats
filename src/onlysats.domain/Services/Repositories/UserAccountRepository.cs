using onlysats.domain.Entity;
using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of User Accounts
/// </summary>
public interface IUserAccountRepository
{
    Task<UserAccount?> GetUserAccount(int id);
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

    public async Task<UserAccount?> GetUserAccount(int id)
    {
        const string sql = $@"";

        return await _Repository.SelectSingle<UserAccount>(sql);
    }

    public async Task<UserAccount> UpsertUserAccount(UserAccount userAccount)
    {
        const string sql = $@"";

        return await _Repository.Upsert<UserAccount>(sql);
    }
}

#endregion