namespace onlysats.domain.Entity;

/// <summary>
/// Various notification settings for the Creator
/// </summary>
public class CreatorNotificationSettings
{
    /// <summary>
    /// The global unique identifier for the Creator's notification settings
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A reference to the Creator
    /// </summary>
    public int CreatorId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool UsePush { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool UseEmail { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool NewMessage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool NewSubscriber { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool NewTip { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool NewPurchase { get; set; }
}
