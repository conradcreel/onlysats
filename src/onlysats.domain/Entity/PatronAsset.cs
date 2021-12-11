namespace onlysats.domain.Entity;


/// <summary>
/// Stores the assets a Patron has purchased from a Creator. Once a Patron purchases an 
/// asset, they can access it indefinitely.
/// </summary>
public class PatronAsset
{

    /// <summary>
    /// The global unique identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Patron who owns a copy of the Asset
    /// </summary>
    public int PatronId { get; set; }

    /// <summary>
    /// The Creator of the Asset
    /// </summary>
    public int CreatorId { get; set; }

    /// <summary>
    /// A reference to the Payment that provided the Patron with this Asset
    /// </summary>
    public int PaymentId { get; set; }

    /// <summary>
    /// A reference to the Asset the Patron purchased
    /// </summary>
    public int AssetId { get; set; }

    /// <summary>
    /// Optional but common package name the purchase of this Asset was associated with
    /// </summary>
    public string? AssetPackageName { get; set; }

    /// <summary>
    /// The timestamp of when the Patron acquired this Asset
    /// </summary>
    public DateTime DateAcquired { get; set; }
}
