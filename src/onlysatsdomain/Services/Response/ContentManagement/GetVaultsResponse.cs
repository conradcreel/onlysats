using System.Collections.Generic;
using onlysats.domain.Models;
using onlysats.domain.Services.Response;

namespace onlysats.domain.Services.Response.ContentManagement
{
    public class GetVaultsResponse : ResponseBase
    {
        public int CreatorId { get; set; }
        public List<VaultModel> Vaults { get; set; } = new List<VaultModel>();
        public int Top { get; set; }
        public int Skip { get; set; }
        public int Total { get; set; }
    }
}