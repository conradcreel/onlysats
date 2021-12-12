using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of User Accounts
/// </summary>
public interface IUserAccountRepository
{

}

#region Implementation

public class UserAccountRepository : IUserAccountRepository 
{
    private readonly ISqlRepository _Repository;

    public UserAccountRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }
}

#endregion 