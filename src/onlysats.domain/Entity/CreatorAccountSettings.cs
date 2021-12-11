namespace onlysats.domain.Entity;

/// <summary>
/// Various account settings for the Creator
/// </summary>
public class CreatorAccountSettings
{
    /// <summary>
    /// The global unique identifier for the Creator's account settings
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A reference to the Creator
    /// </summary>
    public int CreatorId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool ShowActivityStatus { get; set; }
    // TODO: More
}
