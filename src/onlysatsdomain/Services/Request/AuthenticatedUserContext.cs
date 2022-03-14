using onlysats.domain.Enums;

namespace onlysats.domain.Services.Request
{
    /// <summary>
    /// Populated from access token and supplied to every
    /// Request object
    /// </summary>
    public class AuthenticatedUserContext
    {
        public int UserAccountId { get; set; }
        public string Username { get; set; }
        public EUserRole UserRole { get; set; }
    }
}