
using System.Collections.Generic;
using onlysats.domain.Models;

namespace onlysats.domain.Services.Response.ContentManagement
{
    public class GetPatronAssetsResponse : ResponseBase
    {
        public List<PatronAssetModel> Assets { get; set; }
    }
}