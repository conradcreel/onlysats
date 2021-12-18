namespace onlysats.domain.Entity;

/// <summary>
/// Various notification settings for the Creator
/// </summary>
public class CreatorNotificationSettings : BaseEntity
{
    /// <summary>
    /// A reference to the Creator
    /// </summary>
    public int CreatorId { get; set; }

    /// <summary>
    /// Use push notifications for notifications
    /// </summary>
    public bool UsePush { get; set; }

    /// <summary>
    /// Use email for notifications
    /// </summary>
    public bool UseEmail { get; set; }

    /// <summary>
    /// Receive a notification when a new Chat message is received
    /// </summary>
    public bool NewMessage { get; set; }

    /// <summary>
    /// Receive a notification for each new subscriber
    /// </summary>
    public bool NewSubscriber { get; set; }

    /// <summary>
    /// Receive a notification for any tip
    /// </summary>
    public bool NewTip { get; set; }

    /// <summary>
    /// Receive a notification for any purchase
    /// </summary>
    public bool NewPurchase { get; set; }
}
