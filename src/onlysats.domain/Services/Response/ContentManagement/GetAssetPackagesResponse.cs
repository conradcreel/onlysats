using onlysats.domain.Models;
using onlysats.domain.Services.Response;

namespace onlysats.domain.Services.Response.ContentManagement;

public class GetAssetPackagesResponse : ResponseBase
{
    public int CreatorId { get; set; }
    public List<AssetPackageModel> AssetPackages { get; set; } = new List<AssetPackageModel>();
    public int Total { get; set; }
    public int Top { get; set; }
    public int Skip { get; set; }
    // Page num = Skip/Top + 1
}