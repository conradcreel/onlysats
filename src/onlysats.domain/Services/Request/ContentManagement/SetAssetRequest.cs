using onlysats.domain.Enums;

namespace onlysats.domain.Services.Request.ContentManagement;

public class SetAssetRequest : RequestBase
{
    public int CreatorId { get; set; }
    public int? AssetId { get; set; }
    public Include<int>? VaultId { get; set; }
    public Include<string>? FolderName { get; set; }
    public Include<EAssetType>? Type { get; set; }
    public Include<string>? DisplayName { get; set; }
    public Include<string>? Description { get; set; }
    public Include<string>? LocalHash { get; set; }
    public Include<string>? RemoteLocation { get; set; }
    public Include<string>? BlobId { get; set; }
    public Include<EAssetStatus>? Status { get; set; }

    public override bool IsValid()
    {
        if (CreatorId <= 0)
        {
            return false;
        }

        if (AssetId.HasValue)
        {
            if (AssetId.Value <= 0)
            {
                return false;
            }

            // Updating an existing Asset
            // At least one field must be supplied and valid
            return (VaultId != null && VaultId.Value > 0) ||
                    (FolderName != null && !string.IsNullOrWhiteSpace(FolderName.Value)) ||
                    Type != null ||
                    (DisplayName != null && !string.IsNullOrWhiteSpace(DisplayName.Value)) ||
                    (Description != null && !string.IsNullOrWhiteSpace(Description.Value)) ||
                    (LocalHash != null && !string.IsNullOrWhiteSpace(LocalHash.Value)) ||
                    (RemoteLocation != null && !string.IsNullOrWhiteSpace(RemoteLocation.Value)) ||
                    (BlobId != null && !string.IsNullOrWhiteSpace(BlobId.Value)) ||
                    Status != null;
        }
        else
        {
            // Creating a new Asset
            // All fields except Description must be valid
            return VaultId != null && VaultId.Value > 0 &&
                    FolderName != null && !string.IsNullOrWhiteSpace(FolderName.Value) &&
                    Type != null &&
                    DisplayName != null && !string.IsNullOrWhiteSpace(DisplayName.Value) &&
                    LocalHash != null && !string.IsNullOrWhiteSpace(LocalHash.Value) &&
                    RemoteLocation != null && !string.IsNullOrWhiteSpace(RemoteLocation.Value) &&
                    BlobId != null && !string.IsNullOrWhiteSpace(BlobId.Value) &&
                    Status != null;
        }
    }
}