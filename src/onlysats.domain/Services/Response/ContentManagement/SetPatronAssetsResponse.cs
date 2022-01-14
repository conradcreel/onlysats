using onlysats.domain.Models;

namespace onlysats.domain.Services.Response.ContentManagement;

public class SetPatronAssetsResponse : ResponseBase
{
    public List<PatronAssetModel> Assets { get; set; }
}