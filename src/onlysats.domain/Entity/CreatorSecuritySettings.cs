namespace onlysats.domain.Entity;

/// <summary>
/// Various security settings for the Creator
/// </summary>
public class CreatorSecuritySettings : BaseEntity
{
    /// <summary>
    /// A reference to the Creator
    /// </summary>
    public int CreatorId { get; set; }

    /// <summary>
    /// Show activity status on profile and in chat
    /// </summary>
    public bool ShowActivityStatus { get; set; }

    /// <summary>
    /// Completely hide profile and show a 404
    /// </summary>
    public bool FullyPrivateProfile { get; set; }

    /// <summary>
    /// Display the number of Patrons subscribing on your profile
    /// </summary>
    public bool ShowPatronCount { get; set; }

    /// <summary>
    /// Display your media asset count on your profile
    /// </summary>
    public bool ShowMediaCount { get; set; }

    /// <summary>
    /// Add a watermark to each photo you upload
    /// </summary>
    public bool WatermarkPhotos { get; set; }

    /// <summary>
    /// Add a watermark to each video you upload
    /// </summary>
    public bool WatermarkVideos { get; set; }
}
