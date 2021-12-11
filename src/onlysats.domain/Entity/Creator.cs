namespace onlysats.domain.Entity;

/// <summary>
/// Content creators seeking to sell their content/services to Patrons
/// </summary>
public class Creator
{
    /// <summary>
    /// The global unique identifier of this Creator
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A reference to the UserAccount for this Creator
    /// </summary>
    public int UserAccountId { get; set; }
}
