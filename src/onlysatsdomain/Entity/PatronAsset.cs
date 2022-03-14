using System;

namespace onlysats.domain.Entity
{

    /// <summary>
    /// Stores the assets a Patron has purchased from a Creator. Once a Patron purchases an 
    /// asset, they can access it indefinitely.
    /// </summary>
    public class PatronAsset : BaseEntity
    {
        /// <summary>
        /// The Patron who owns a copy of the Asset
        /// </summary>
        public int PatronId { get; set; }

        /// <summary>
        /// The Creator of the Asset
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// A reference to the Payment that provided the Patron with this Asset
        /// if relevant
        /// </summary>
        public int? PaymentId { get; set; }

        /// <summary>
        /// A reference to the Asset the Patron purchased
        /// </summary>
        public int AssetId { get; set; }

        /// <summary>
        /// Optional but common package name the purchase of this Asset was associated with
        /// </summary>
        public string AssetPackageName { get; set; }

        /// <summary>
        /// This URI will contain the SAS for this specific Patron. 
        /// Note: Could consider removing this in the future and generating SAS on the fly each time but that could be 
        /// non-performant depending on number of Assets being loaded at once. While it's true
        /// if this link is shared, the content can be shared with anyone (even to those not 
        /// using OnlySats), a malicious Patron could also save the content locally and distribute
        /// on their own so it's considered an acceptable risk at the moment. 
        /// </summary>
        public string UniqueAssetUri { get; set; }

        /// <summary>
        /// The timestamp of when the Patron acquired this Asset
        /// </summary>
        public DateTime DateAcquired { get; set; }
    }
}