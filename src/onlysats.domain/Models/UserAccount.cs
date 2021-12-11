namespace onlysats.domain.Models;

public class UserAccount
{
    public int Id { get; set; }
    public UserRole Role { get; set; }
    public string UserId { get; set; } = string.Empty; // From IdP
}
