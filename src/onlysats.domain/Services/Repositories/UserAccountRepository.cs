using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
///
/// </summary>
public interface IUserAccountRepository
{

}

#region Implementation

public class UserAccountRepository : IUserAccountRepository 
{
    private readonly SqlRepository _Repository;

    public UserAccountRepository(OnlySatsConfiguration config)
    {
        _Repository = new SqlRepository(config.SqlConnectionString);
    }
}

#endregion 