using onlysats.domain.Enums;

namespace onlysats.domain.Entity;

/// <summary>
/// A post in a Creator's feed
/// </summary>
public class FeedPost : BaseEntity
{
    /// <summary>
    /// The Creator who created this post
    /// </summary>
    public int CreatorId { get; set; }

    /// <summary>
    /// When the post was updated (posted, visibility changed, etc.)
    /// </summary>
    public DateTime DateUpdated { get; set; }

    /// <summary>
    /// How many Patrons have liked this post
    /// </summary>
    public int LikeCount { get; set; }

    /// <summary>
    /// The content of the post. Typically contains some media and a link
    /// to purchase some asset
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// The status of this post, e.g. active, deleted, etc.
    /// </summary>
    public EFeedPostStatus Status { get; set; }

    /// <summary>
    /// Who can view this post, e.g. the public, subscribing patrons, only creator
    /// </summary>
    public EFeedPostVisibility Visibility { get; set; }
}