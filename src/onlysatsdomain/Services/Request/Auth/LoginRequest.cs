using System;

namespace onlysats.domain.Services.Request.Auth
{
    public class LoginRequest : RequestBase
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                    !string.IsNullOrWhiteSpace(Password);
        }
    }
}