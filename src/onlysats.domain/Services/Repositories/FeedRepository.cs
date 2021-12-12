using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
///
/// </summary>
public interface IFeedRepository
{

}

#region Implementation

public class FeedRepository : IFeedRepository 
{
    private readonly SqlRepository _Repository;

    public FeedRepository(OnlySatsConfiguration config)
    {
        _Repository = new SqlRepository(config.SqlConnectionString);
    }
}

#endregion 