
using System;
using System.Collections.Generic;

namespace onlysats.domain.Models;

public class CreatorFeed
{
    public List<FeedPost>? Posts { get; set; }
}

public class FeedPost
{
    public string Id { get; set; } = string.Empty;
    public DateTime DatePosted { get; set; }
    public List<string>? PatronLikes { get; set; }
    public string Content { get; set; } = string.Empty;
}
