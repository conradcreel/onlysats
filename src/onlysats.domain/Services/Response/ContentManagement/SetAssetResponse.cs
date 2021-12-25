using onlysats.domain.Models;
using onlysats.domain.Services.Response;

namespace onlysats.domain.Services.ContentManagement;

public class SetAssetResponse : ResponseBase
{
    public AssetModel Asset { get; set; }
}