namespace onlysats.domain.Services.Request.Onboarding;

public class UpdateCreatorSettingsRequest : RequestBase
{
    public int CreatorId { get; set; }

    // Chat Settings
    public Include<bool>? ChatShowWelcomeMessage { get; set; }
    public Include<bool>? ChatHideOutgoingMassMessages { get; set; }

    // Notification Settings
    public Include<bool>? NotificationUsePush { get; set; }
    public Include<bool>? NotificationUseEmail { get; set; }
    public Include<bool>? NotificationNewMessage { get; set; }
    public Include<bool>? NotificationNewSubscriber { get; set; }
    public Include<bool>? NotificationNewTip { get; set; }
    public Include<bool>? NotificationNewPurchase { get; set; }

    // Profile Settings
    public Include<string>? DisplayName { get; set; }
    public Include<string>? CoverPhotoUri { get; set; }
    public Include<string>? ProfilePhotoUri { get; set; }
    public Include<double>? SubscriptionPricePerMonth { get; set; }
    public Include<string>? AboutHtml { get; set; }
    public Include<string>? AmazonWishList { get; set; }

    // Security Settings
    public Include<bool>? ShowActivityStatus { get; set; }
    public Include<bool>? FullyPrivateProfile { get; set; }
    public Include<bool>? ShowPatronCount { get; set; }
    public Include<bool>? ShowMediaCount { get; set; }
    public Include<bool>? WatermarkPhotos { get; set; }
    public Include<bool>? WatermarkVideos { get; set; }

    public override bool IsValid()
    {
        return CreatorId > 0 &&
            (
                ChatShowWelcomeMessage != null ||
                ChatHideOutgoingMassMessages != null ||
                NotificationUsePush != null ||
                NotificationUseEmail != null ||
                NotificationNewMessage != null ||
                NotificationNewSubscriber != null ||
                NotificationNewTip != null ||
                NotificationNewPurchase != null ||
                DisplayName != null ||
                CoverPhotoUri != null ||
                ProfilePhotoUri != null ||
                SubscriptionPricePerMonth != null ||
                AboutHtml != null ||
                AmazonWishList != null ||
                ShowActivityStatus != null ||
                FullyPrivateProfile != null ||
                ShowPatronCount != null ||
                ShowMediaCount != null ||
                WatermarkPhotos != null ||
                WatermarkVideos != null
            );
    }
}