namespace onlysats.domain.Entity
{
    /// <summary>
    /// Various profile settings for the Creator
    /// </summary>
    public class CreatorProfileSettings : BaseEntity
    {
        /// <summary>
        /// A reference to the Creator
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// Display name on profile underneath username
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Image uri to use as cover photo
        /// </summary>
        public string CoverPhotoUri { get; set; }

        /// <summary>
        /// Image uri to use as profile photo
        /// </summary>
        public string ProfilePhotoUri { get; set; }

        /// <summary>
        /// The price in the Creator's selected currency (which will be converted to Sats)
        /// that will be pulled from Patron each month until they cancel
        /// </summary>
        public double SubscriptionPricePerMonth { get; set; }

        /// <summary>
        /// About section for Creator's profile
        /// </summary>
        public string AboutHtml { get; set; }

        /// <summary>
        /// Link to Creator's Amazon wishlist
        /// </summary>
        public string AmazonWishList { get; set; }

        /// <summary>
        /// Show "Chat with Me" link on Profile
        /// </summary>
        public bool ChatEnabled { get; set; }

        /// <summary>
        /// Show "Subscribe" link on Profile
        /// </summary>
        public bool SubscribeEnabled { get; set; }
    }
}