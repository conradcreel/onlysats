using onlysats.domain.Enums;

namespace onlysats.domain.Entity;

/// <summary>
/// Creators can run promotions on content. These promotions can 
/// then be sent out as mass messages to all (or a subset) of 
/// subscribers. A Promotion is also used in individual chat messages
/// when content is shared 
/// </summary>
public class Promotion : BaseEntity
{
    /// <summary>
    /// The Creator releasing this promotion
    /// </summary>
    public int CreatorId { get; set; }

    /// <summary>
    /// The name of this promotion 
    /// Ex: 50% off moonlight collection
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// A description of the promotion, what it contains, etc.
    /// An additional field to include more verbose information
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The price in the Creator's selected currency (which will be converted to Sats)
    /// that this promotion can be purchased at
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// The Asset Packages included in this promotion
    /// </summary>
    public List<int>? AssetPackageIds { get; set; }

    /// <summary>
    /// The Assets not already included in a package included in this promotion
    /// </summary>
    public List<int>? AssetIds {get;set;}

    /// <summary>
    /// The earliest this promotion is valid. If null,
    /// it is immediately available
    /// </summary>
    public DateTime? BeginDateUtc { get; set; }

    /// <summary>
    /// The latest this promotion is valid. If null,
    /// it is available indefinitely
    /// </summary>
    public DateTime? EndDateUtc { get; set; }

    /// <summary>
    /// Once this Promotion is sent at least once, the 
    /// Creator cannot change anything about it except deactivate
    /// it so it can not be purchased further.
    /// </summary>
    public bool IsLocked { get; set; }

    /// <summary>
    /// The status of this promotion
    /// </summary>
    public EPromotionStatus Status { get; set; }

    /// <summary>
    /// This will enable this Promotion to be listed in the Creator's store
    /// </summary>
    public bool IncludeInStore { get; set; }
}