namespace onlysats.domain.Models;

public class Creator
{
    public int Id { get; set; }
    public int UserAccountId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public CreatorSettings? Settings { get; set; }
    public CreatorNotificationPreferences? Notifications { get; set; }
    public CreatorFeed? Feed { get; set; }
}
