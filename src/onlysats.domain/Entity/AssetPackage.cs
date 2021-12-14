using onlysats.domain.Enums;

namespace onlysats.domain.Entity;

/// <summary>
/// A grouping of Assets which can be purchased like an e-commerce product or 
/// via a private negotiation on a per-Patron basis. Useful for deals/promos
/// </summary>
public class AssetPackage : BaseEntity
{
    /// <summary>
    /// A reference to the Vault where this AssetPackage is stored
    /// Note an AssetPackage can only contain Assets from the same 
    /// Vault. However, those Assets can be in different folders 
    /// within that Vault. This is primarily for a smoother UX
    /// </summary>
    public int VaultId { get; set; }

    /// <summary>
    /// The name that is displayed when this asset package is loaded
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// The description that is displayed when this asset is loaded
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The Assets contained within this package
    /// </summary>
    public List<int>? AssetIds { get; set; }

    /// <summary>
    /// The Id of the Asset that will be publicly viewable as a 
    //  teaser for this package. Could be used as a package cover
    //  photo
    /// </summary>
    public int TeaserAssetId { get; set; }

    /// <summary>
    /// The price in the Creator's selected currency (which will be converted to Sats)
    /// that this package can be purchased at by Patrons like an e-comm store.
    /// </summary>
    public double BuyNowPrice { get; set; }

    /// <summary>
    /// Once this Asset Package is purchased at least once, the 
    /// Creator cannot change anything about it except deactivate
    /// it so it can not be purchased further.
    /// </summary>
    public bool IsLocked { get; set; }

    /// <summary>
    /// The status of this Asset Package. As long as it's in Draft status, it 
    /// will not be visible to anyone but the Creator. Make sure you're satisfied 
    /// with the Asset Package prior to Activating it. 
    /// </summary>
    public EAssetPackageStatus Status { get; set; }
}
