using onlysats.domain.Entity;

namespace onlysats.domain.Models;

public class VaultModel
{
    public VaultModel(Vault vault, string creatorUserName)
    {
        Creator = creatorUserName;
        CreatorId = vault.CreatorId;
        VaultId = vault.Id;
        Name = vault.Name;
        Folders = vault.Folders;
        Description = vault.Description;
    }

    public VaultModel(Vault vault, string creatorUserName, IEnumerable<Asset> assets) : this(vault, creatorUserName)
    {
        Assets = assets.Select(a => new AssetModel(a, creatorUserName)).ToList();
    }

    public string Creator { get; }
    public int CreatorId { get; }
    public int VaultId { get; }
    public string Name { get; }
    public List<string> Folders { get; } = new List<string>();

    public string? Description { get; }

    public List<AssetModel>? Assets { get; }
}