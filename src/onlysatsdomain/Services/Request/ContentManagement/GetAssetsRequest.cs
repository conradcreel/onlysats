using System.Collections.Generic;

namespace onlysats.domain.Services.Request.ContentManagement
{
    public class GetAssetsRequest : RequestBase
    {
        public int CreatorId { get; set; }
        public List<int> AssetIds { get; set; } = new List<int>(); // Empty means all
        public int? VaultId { get; set; }
        public int Top { get; set; } = 10;
        public int Skip { get; set; } = 0;

        public override bool IsValid()
        {
            return CreatorId > 0;
        }
    }
}