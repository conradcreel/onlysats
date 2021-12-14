namespace onlysats.domain.Entity;

/// <summary>
/// A notification shown to a user
/// </summary>
public class Notification : BaseEntity
{
    /// <summary>
    /// The UserAccount to show the Notification to
    /// </summary>
    public int UserAccountId { get; set; }

    /// <summary>
    /// The message to display
    /// </summary>
    public string Message {get;set;} = string.Empty;

    /// <summary>
    /// Notifications can have internal CTAs which can be things such as 
    /// viewing a payment, going to a chat message, etc. Just shortcuts
    /// to help the user out when they're viewing a notification and 
    /// want to go to the area that raised the notification
    /// </summary>
    public string InternalCallToActionLink {get;set;} = string.Empty;
}