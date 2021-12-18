namespace onlysats.domain.Models;

/// <summary>
/// Full model of a Creator including their settings
/// </summary>
public class CreatorModel
{
    public int UserAccountId { get; set; }
    public int CreatorId { get; set; }
    public string? Username { get; set; }
    public string? DisplayName { get; set; }
    public string? Email { get; set; }

    /// <summary>
    /// Image uri to use as cover photo
    /// </summary>
    public string? CoverPhotoUri { get; set; }

    /// <summary>
    /// Image uri to use as profile photo
    /// </summary>
    public string? ProfilePhotoUri { get; set; }

    /// <summary>
    /// The price in the Creator's selected currency (which will be converted to Sats)
    /// that will be pulled from Patron each month until they cancel
    /// </summary>
    public double SubscriptionPricePerMonth { get; set; }

    /// <summary>
    /// About section for Creator's profile
    /// </summary>
    public string? AboutHtml { get; set; }

    /// <summary>
    /// Link to Creator's Amazon wishlist
    /// </summary>
    public string? AmazonWishList { get; set; }

    public ChatSettingsModel? ChatSettings { get; set; }
    public NotificationSettingsModel? NotificationSettings { get; set; }
    public SecuritySettingsModel? SecuritySettings { get; set; }
}

public class ChatSettingsModel
{
    /// <summary>
    /// Upon receiving a new subscriber, the subscriber 
    /// can be sent a welcome chat message
    /// </summary>
    public bool ShowWelcomeMessage { get; set; }

    // TODO: A property for the Welcome message. Look into how 
    // chat messages in Matrix are formatted 

    /// <summary>
    /// Outgoing mass messages will not show in the chat list
    /// until the subscriber responds if this property is set
    /// </summary>
    public bool HideOutgoingMassMessages { get; set; }
}
public class NotificationSettingsModel
{
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

public class SecuritySettingsModel
{
    /// <summary>
    /// Show activity status on profile and in chat
    /// </summary>
    public bool ShowActivityStatus { get; set; }

    /// <summary>
    /// Completely hide profile and show a 404
    /// </summary>
    public bool FullyPrivateProfile { get; set; }

    /// <summary>
    /// Display the number of Patrons subscribing on your profile
    /// </summary>
    public bool ShowPatronCount { get; set; }

    /// <summary>
    /// Display your media asset count on your profile
    /// </summary>
    public bool ShowMediaCount { get; set; }

    /// <summary>
    /// Add a watermark to each photo you upload
    /// </summary>
    public bool WatermarkPhotos { get; set; }

    /// <summary>
    /// Add a watermark to each video you upload
    /// </summary>
    public bool WatermarkVideos { get; set; }
}