using System.Collections.Generic;
using System.Linq;

namespace onlysats.domain.Services.Request.ContentManagement
{
    public class SetVaultRequest : RequestBase
    {
        public int CreatorId { get; set; }
        public int? VaultId { get; set; }

        public Include<string> Name { get; set; }
        public Include<List<string>> FoldersToAdd { get; set; }
        public Include<List<string>> FoldersToRemove { get; set; }
        public Include<string> Description { get; set; }

        public override bool IsValid()
        {
            if (CreatorId <= 0)
            {
                return false;
            }
            
            if (VaultId.HasValue)
            {
                if (VaultId.Value <= 0)
                {
                    return false;
                }

                // Updating an existing Vault
                // At least one of the fields must be included and valid
                return (Name != null && !string.IsNullOrWhiteSpace(Name.Value)) ||
                        (FoldersToAdd != null && FoldersToAdd.Value.Any()) ||
                        (FoldersToRemove != null && FoldersToRemove.Value.Any()) ||
                        (Description != null && !string.IsNullOrWhiteSpace(Description.Value));
            }
            else
            {
                // Creating a new Vault
                // All fields except FoldersToRemove and Description must be included and valid

                return Name != null && !string.IsNullOrWhiteSpace(Name.Value) &&
                       FoldersToAdd != null && FoldersToAdd.Value.Any() &&
                       Description != null && !string.IsNullOrWhiteSpace(Description.Value);

            }
        }
    }
}