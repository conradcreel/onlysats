using System.Reflection.Metadata;
using onlysats.domain.Entity;

namespace onlysats.domain.Models;

public class AssetModel
{
    public AssetModel(Asset assetEntity, string creatorUserName)
    {
        Creator = creatorUserName;
        VaultId = assetEntity.VaultId;
        AssetId = assetEntity.Id;
        FolderName = assetEntity.FolderName;
        Type = assetEntity.Type.ToString();
        DisplayName = assetEntity.DisplayName;
        Description = assetEntity.Description;
        LocalHash = assetEntity.LocalHash;
        RemoteLocation = assetEntity.RemoteLocation;
        BlobId = assetEntity.BlobId;
    }

    public string Creator { get; } // This is the Container name in Blob Storage
    public int VaultId { get; }
    public int AssetId { get; }
    public string FolderName { get; }
    public string Type { get; } // Image, Video, etc.
    public string DisplayName { get; }
    public string? Description { get; }
    public string LocalHash { get; }
    public string RemoteLocation { get; }
    public string BlobId { get; }
}