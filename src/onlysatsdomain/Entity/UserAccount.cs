using onlysats.domain.Enums;

namespace onlysats.domain.Entity
{

    /// <summary>
    /// User related info for identification and authentication purposes
    /// </summary>
    public class UserAccount : BaseEntity
    {
        /// <summary>
        /// The displayname that is shown in Chat, Profile, Feed, etc.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The email address to send communications to
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// A user can only be of one type--a Creator or a Patron
        /// </summary>
        public EUserRole Role { get; set; }

        /// <summary>
        /// The Identifier in the external IdP the user authenticated with
        /// </summary>
        public string UserId { get; set; } = string.Empty; // From IdP
        /// <summary>
        /// The IdP the user authenticated with. E.g. Auth0
        /// </summary>
        public string IdpSource { get; set; } = string.Empty;
    }
}