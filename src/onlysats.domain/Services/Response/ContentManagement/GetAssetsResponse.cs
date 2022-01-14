using onlysats.domain.Models;
using onlysats.domain.Services.Response;

namespace onlysats.domain.Services.Response.ContentManagement;

public class GetAssetsResponse : ResponseBase
{
    public int CreatorId { get; set; }
    public List<AssetModel> Assets { get; set; } = new List<AssetModel>();
    public int Total { get; set; }
    public int Top { get; set; }
    public int Skip { get; set; }
    // Page num = Skip/Top + 1
}