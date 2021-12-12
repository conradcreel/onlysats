using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of Feed Posts
/// </summary>
public interface IFeedRepository
{

}

#region Implementation

public class FeedRepository : IFeedRepository 
{
    private readonly ISqlRepository _Repository;

    public FeedRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }
}

#endregion 