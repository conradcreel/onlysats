namespace onlysats.domain.Models;

public class Vault
{
    public int Id { get; set; }
    public int CreatorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<Asset>? Assets { get; set; }
    public List<AssetPackage>? Packages { get; set; }

}
