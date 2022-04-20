using onlysats.domain.Enums;

namespace onlysats.domain.Services.Response.Auth
{
    public class LoginResponse : ResponseBase
    {
        public int UserAccountId { get; set; }
        public EUserRole Role { get; set; }
        public string ChatAccessToken { get; set; }
        public string Username { get; set; }
    }
}