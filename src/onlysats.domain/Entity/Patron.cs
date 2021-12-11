namespace onlysats.domain.Entity;

/// <summary>
/// Subscribers/Fans of Creator(s)
/// </summary>
public class Patron
{
    /// <summary>
    /// The global unique identifier of this Patron
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A reference to the UserAccount for this Patron
    /// </summary>
    public int UserAccountId { get; set; }

    /// <summary>
    /// For deployments that require patrons to be a member to 
    /// interact, when does their membership expire?
    /// </summary>
    public DateTime? MemberUntil { get; set; }
}
