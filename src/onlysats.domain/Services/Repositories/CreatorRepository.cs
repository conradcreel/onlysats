using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
///
/// </summary>
public interface ICreatorRepository
{

}

#region Implementation

public class CreatorRepository : ICreatorRepository 
{
    private readonly SqlRepository _Repository;

    public CreatorRepository(OnlySatsConfiguration config)
    {
        _Repository = new SqlRepository(config.SqlConnectionString);
    }
}

#endregion 