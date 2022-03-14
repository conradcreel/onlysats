using System.Collections.Generic;
using System.Linq;

namespace onlysats.domain.Services.Request.ContentManagement
{
    public class SetPatronAssetsRequest : RequestBase
    {
        public int PatronId { get; set; }
        public int CreatorId { get; set; }
        public int? PaymentId { get; set; }
        public List<int> AssetIds { get; set; } = new List<int>();
        public string AssetPackageName { get; set; }

        public override bool IsValid()
        {
            return PatronId > 0 && CreatorId > 0 && AssetIds.Any();
        }
    }
}