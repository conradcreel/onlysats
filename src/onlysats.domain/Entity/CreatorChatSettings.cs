namespace onlysats.domain.Entity;

/// <summary>
/// Various chat settings for the Creator
/// </summary>
public class CreatorChatSettings
{
    /// <summary>
    /// The global unique identifier for the Creator's chat settings
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A reference to the Creator
    /// </summary>
    public int CreatorId { get; set; }
    // TODO: More
}
