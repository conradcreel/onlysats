using onlysats.domain.Constants;
using onlysats.domain.Enums;

namespace onlysats.domain.Entity;

/// <summary>
/// The content that makes up an AssetPackage that is ultimately sold or otherwise 
/// transferred to Patrons. Assets cannot be purchased directly but can be grouped
/// together in an unlimited number of packages to be sold/transferred
/// </summary>
public class Asset : BaseEntity
{
    /// <summary>
    /// For convenience, store the Creator of this Asset even though we can 
    /// retrieve it from the Vault this Asset belongs to
    /// </summary>
    public int CreatorId { get; set; }

    /// <summary>
    /// A reference to the Vault where this Asset is stored
    /// </summary>
    public int VaultId { get; set; }

    /// <summary>
    /// The folder in the Vault that this Asset is tagged with
    /// Useful for organization in the UI
    /// </summary>
    public string FolderName { get; set; } = CDefaults.NO_FOLDER_NAME;

    /// <summary>
    /// What type of asset is this? e.g. Image, Video, etc.
    /// </summary>
    public EAssetType Type { get; set; }

    /// <summary>
    /// The name that is displayed when this asset is loaded
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// An optional description that is displayed when this asset is loaded
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// A hash of the contents of this file to verify authenticity and identification
    /// of duplicates/quick access from Blob storage
    /// </summary>
    public string LocalHash { get; set; } = string.Empty;

    /// <summary>
    /// Full link to the protected remote location of this asset
    /// </summary>
    public string RemoteLocation { get; set; } = string.Empty;

    /// <summary>
    /// The Id of this asset in Blob Storage
    /// </summary>
    public string BlobId { get; set; } = string.Empty;

    /// <summary>
    /// The current status of this Asset. e.g. active or inactive.
    /// Note that if a Patron purchases an Asset, they have access 
    /// to it indefinitely, regardless if you deactivate it.
    /// </summary>
    public EAssetStatus Status { get; set; }
}
