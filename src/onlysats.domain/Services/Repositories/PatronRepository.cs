using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
///
/// </summary>
public interface IPatronRepository
{

}

#region Implementation

public class PatronRepository : IPatronRepository 
{
    private readonly SqlRepository _Repository;

    public PatronRepository(OnlySatsConfiguration config)
    {
        _Repository = new SqlRepository(config.SqlConnectionString);
    }
}

#endregion 