using Dapr.Client;
using onlysats.domain.Services.Repositories;

namespace onlysats.domain.Services;

/// <summary>
/// Handles all user engagement operations such as Notifications 
/// within OnlySats and Marketing functions from Creators to 
/// to Patrons in an effort to increase (Patron) engagement
/// </summary>
public interface IUserEngagementService
{

}

#region Implementation

public class UserEngagementService : IUserEngagementService
{
    private readonly INotificationRepository _NotificationRepository;
    private readonly MessagePublisherProxy _MessagePublisher;

    public UserEngagementService(INotificationRepository notificationRepository,
                                MessagePublisherProxy messagePublisher)
    {
        _NotificationRepository = notificationRepository;
        _MessagePublisher = messagePublisher;
    }

}

#endregion