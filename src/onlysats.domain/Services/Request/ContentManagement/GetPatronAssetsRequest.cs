namespace onlysats.domain.Services.Request.ContentManagement;

public class GetPatronAssetsRequest : RequestBase
{
    public int PatronId { get; set; }
    public List<int>? AssetIds { get; set; }
    public string? AssetPackageName { get; set; }
    public int? CreatorId { get; set; }
    public int Top { get; set; } = 10;
    public int Skip { get; set; } = 0;

    public override bool IsValid()
    {
        return PatronId > 0;
    }
}