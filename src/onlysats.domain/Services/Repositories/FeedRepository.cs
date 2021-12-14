using onlysats.domain.Entity;
using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of Feed Posts
/// </summary>
public interface IFeedRepository
{
    /// <summary>
    /// Retrieves a FeedPost by its Id
    /// </summary>
    Task<FeedPost> GetFeedPost(int id);

    /// <summary>
    /// Constructs a CreatorFeed or the supplied CreatorId from the top/skip 
    /// FeedPosts
    /// </summary>
    Task<CreatorFeed> GetCreatorFeed(int creatorId, int top, int skip);

    /// <summary>
    /// Updates or Inserts a FeedPost
    /// </summary>
    Task<FeedPost> UpsertFeedPost(FeedPost feedPost);
}

#region Implementation

public class FeedRepository : IFeedRepository
{
    private readonly ISqlRepository _Repository;

    public FeedRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }

    public Task<CreatorFeed> GetCreatorFeed(int creatorId, int top, int skip)
    {
        throw new NotImplementedException();
    }

    public Task<FeedPost> GetFeedPost(int id)
    {
        throw new NotImplementedException();
    }

    public Task<FeedPost> UpsertFeedPost(FeedPost feedPost)
    {
        throw new NotImplementedException();
    }
}

#endregion