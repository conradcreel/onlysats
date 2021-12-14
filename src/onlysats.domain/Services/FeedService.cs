using Dapr.Client;
using onlysats.domain.Services.Repositories;

namespace onlysats.domain.Services;

/// <summary>
/// Manages FeedPosts and constructs a Feed for on-demand fetching
/// </summary>
public interface IFeedService
{

}

#region Implementation

public class FeedService : IFeedService
{
    private readonly IFeedRepository _FeedRepository;
    private readonly MessagePublisherProxy _MessagePublisher;

    public FeedService(IFeedRepository feedRepository,
                      MessagePublisherProxy messagePublisher)
    {
        _FeedRepository = feedRepository;
        _MessagePublisher = messagePublisher;
    }
}

#endregion
