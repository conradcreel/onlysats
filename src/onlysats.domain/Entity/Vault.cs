using onlysats.domain.Constants;

namespace onlysats.domain.Entity;

/// <summary>
/// All Creator Assets are stored in a Vault. A Creator can have multiple vaults
/// </summary>
public class Vault
{
    /// <summary>
    /// The global unique identifier of this vault
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The owner of this vault
    /// </summary>
    public int CreatorId { get; set; }

    /// <summary>
    /// The display name of this vault. Only visible to the Creator (owner)
    /// </summary>
    public string Name { get; set; } = CDefaults.DEFAULT_VAULT_NAME;

    /// <summary>
    /// The folders (tags) that can be applied to Assets and Packages in this vault
    /// for organizational purposes in the UI
    /// </summary>
    public List<string> Folders { get; set; } = new List<string> { string.Empty };

    /// <summary>
    /// An optional description of the assets contained in this vault
    /// </summary>
    public string? Description { get; set; }

}
