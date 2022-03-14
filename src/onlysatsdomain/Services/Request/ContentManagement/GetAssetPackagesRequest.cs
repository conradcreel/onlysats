using System.Collections.Generic;

namespace onlysats.domain.Services.Request.ContentManagement
{
    public class GetAssetPackagesRequest : RequestBase
    {
        public int CreatorId { get; set; }
        public List<int> AssetPackageIds { get; set; } = new List<int>(); // Empty means all
        public int Top { get; set; } = 10;
        public int Skip { get; set; } = 0;

        public int? VaultId { get; set; }

        public override bool IsValid()
        {
            return CreatorId > 0;
        }
    }
}