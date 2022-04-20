using System;
using System.Linq;

namespace onlysats.domain.Services.Request.Onboarding
{
    public class SetupPatronRequest : RequestBase
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public DateTime? MemberUntil { get; set; }

        public override bool IsValid()
        {
            return 
                    !string.IsNullOrWhiteSpace(Password) &&
                    !string.IsNullOrWhiteSpace(Email) &&
                    !string.IsNullOrWhiteSpace(Username);
        }
    }
}