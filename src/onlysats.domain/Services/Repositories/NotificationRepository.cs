using onlysats.domain.Models;

namespace onlysats.domain.Services.Repositories;

/// <summary>
/// Encapsulates persistence of Notifications within OnlySats
/// </summary>
public interface INotificationRepository
{

}

#region Implementation

public class NotificationRepository : INotificationRepository 
{
    private readonly ISqlRepository _Repository;

    public NotificationRepository(ISqlRepository sqlRepository)
    {
        _Repository = sqlRepository;
    }
}

#endregion 