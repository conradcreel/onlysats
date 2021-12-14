namespace onlysats.domain.Entity;

/// <summary>
/// Various account settings for the Creator
/// </summary>
public class CreatorAccountSettings : BaseEntity
{
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
