namespace onlysats.domain.Models;

public class CreatorNotificationPreferences
{
    public bool UsePush { get; set; }
    public bool UseEmail { get; set; }
    public bool NewMessage { get; set; }
    public bool NewSubscriber { get; set; }
    public bool NewTip { get; set; }
    public bool NewPurchase { get; set; }
}
