namespace onlysats.domain.Services.Response.Onboarding
{
    public class UpdateCreatorSettingsResponse : ResponseBase
    {
        public bool UpdatedChatSettings { get; set; }
        public bool UpdatedProfileSettings { get; set; }
        public bool UpdatedNotificationSettings { get; set; }
        public bool UpdatedSecuritySettings { get; set; }
    }
}