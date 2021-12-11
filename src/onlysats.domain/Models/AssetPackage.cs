namespace onlysats.domain.Models;

public class AssetPackage
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<int>? AssetIds { get; set; }
}
