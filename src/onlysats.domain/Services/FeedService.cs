using onlysats.domain.Services.Repositories;
using onlysats.domain.Services.Request.Feed;
using onlysats.domain.Services.Response.Feed;

namespace onlysats.domain.Services;

/// <summary>
/// Manages FeedPosts and constructs a Feed for on-demand fetching
/// </summary>
public interface IFeedService
{
    Task<GetFeedResponse> GetFeed(GetFeedRequest request);
    Task<GetPostResponse> GetPost(GetPostRequest request);
    Task<SetPostResponse> SetPost(SetPostRequest request);
}

#region Implementation

public class FeedService : IFeedService
{
    private readonly IFeedRepository _FeedRepository;
    private readonly IMessagePublisher _MessagePublisher;

    public FeedService(IFeedRepository feedRepository,
                      IMessagePublisher messagePublisher)
    {
        _FeedRepository = feedRepository;
        _MessagePublisher = messagePublisher;
    }

    public Task<GetFeedResponse> GetFeed(GetFeedRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<GetPostResponse> GetPost(GetPostRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<SetPostResponse> SetPost(SetPostRequest request)
    {
        throw new NotImplementedException();
    }
}

#endregion
