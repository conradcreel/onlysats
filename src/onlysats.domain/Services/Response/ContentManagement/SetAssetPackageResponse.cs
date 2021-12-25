using onlysats.domain.Models;
using onlysats.domain.Services.Response;

namespace onlysats.domain.Services.ContentManagement;

public class SetAssetPackageResponse : ResponseBase
{
    public AssetPackageModel AssetPackage { get; set; }
}