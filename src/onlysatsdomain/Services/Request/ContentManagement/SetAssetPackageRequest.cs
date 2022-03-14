using System.Collections.Generic;
using System.Linq;
using onlysats.domain.Enums;

namespace onlysats.domain.Services.Request.ContentManagement
{
    public class SetAssetPackageRequest : RequestBase
    {
        public int CreatorId { get; set; }
        public int? AssetPackageId { get; set; }

        public Include<int> VaultId { get; set; }
        public Include<string> DisplayName { get; set; }
        public Include<string> Description { get; set; }
        public Include<List<int>> AssetIdsToAdd { get; set; }
        public Include<List<int>> AssetIdsToRemove { get; set; }
        public Include<string> CoverPhotoUri { get; set; }
        public Include<double> BuyNowPrice { get; set; }
        public Include<bool> IsLocked { get; set; }
        public Include<EAssetPackageStatus> Status { get; set; }

        public override bool IsValid()
        {
            if (CreatorId <= 0)
            {
                return false;
            }

            if (AssetPackageId.HasValue)
            {
                if (AssetPackageId.Value <= 0)
                {
                    return false;
                }

                // Updating existing Asset Package
                // At least one field must be supplied and valid
                return (VaultId != null && VaultId.Value > 0) ||
                        (DisplayName != null && !string.IsNullOrWhiteSpace(DisplayName.Value)) ||
                        (Description != null && !string.IsNullOrWhiteSpace(Description.Value)) ||
                        (AssetIdsToAdd != null && AssetIdsToAdd.Value.Any(a => a > 0)) ||
                        (AssetIdsToRemove != null && AssetIdsToRemove.Value.Any(a => a > 0)) ||
                        (BuyNowPrice != null && BuyNowPrice.Value > 0) ||
                        IsLocked != null ||
                        Status != null;
            }
            else
            {
                // Creating a new Asset Package

                // All fields except CoverPhotoUri must be valid
                return VaultId != null && VaultId.Value > 0 &&
                        DisplayName != null && !string.IsNullOrWhiteSpace(DisplayName.Value) &&
                        Description != null && !string.IsNullOrWhiteSpace(Description.Value) &&
                        AssetIdsToAdd != null && AssetIdsToAdd.Value.Any(a => a > 0) &&
                        BuyNowPrice != null && BuyNowPrice.Value > 0 &&
                        IsLocked != null &&
                        Status != null;
            }
        }
    }
}