namespace onlysats.domain.Entity
{
    /// <summary>
    /// Content creators seeking to sell their content/services to Patrons
    /// </summary>
    public class Creator : BaseEntity
    {
        /// <summary>
        /// A reference to the UserAccount for this Creator
        /// </summary>
        public int UserAccountId { get; set; }
    }
}