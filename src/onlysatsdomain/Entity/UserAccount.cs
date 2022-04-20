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
        /// Synapse username will be "@{Username}:{Homeserver}"
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The user's password hashed by SHA-256
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Never displayed or available to the user. Used to authenticate
        /// with Synapse in the backend. Needs to be plaintext for our 
        /// purposes but is generally safe as Synapse is used as a backend
        /// once the user has securely authenticated with OnlySats
        /// </summary>
        public string ChatPassword { get; set; }

        /// <summary>
        /// The email address to send communications to
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// A user can only be of one type--a Creator or a Patron
        /// </summary>
        public EUserRole Role { get; set; }
    }
}