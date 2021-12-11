namespace onlysats.domain.Entity;

/// <summary>
/// Various profile settings for the Creator
/// </summary>
public class CreatorProfileSettings
{
    /// <summary>
    /// The global unique identifier for the Creator's profile settings
    /// </summary>
    public int Id { get; set; }
 
    /// <summary>
    /// A reference to the Creator
    /// </summary>
    public int CreatorId { get; set; }
    // TODO: More
}
