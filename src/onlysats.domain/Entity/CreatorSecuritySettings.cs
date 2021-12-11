namespace onlysats.domain.Entity;

/// <summary>
/// Various security settings for the Creator
/// </summary>
public class CreatorSecuritySettings
{
    /// <summary>
    /// The global unique identifier for the Creator's security settings
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A reference to the Creator
    /// </summary>
    public int CreatorId { get; set; }
    // TODO: More
}
