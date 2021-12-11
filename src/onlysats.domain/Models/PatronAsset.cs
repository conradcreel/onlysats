namespace onlysats.domain.Models;

public class PatronAsset
{
    public int CreatorId { get; set; }
    public Asset? Asset { get; set; }
    public DateTime DateAcquired { get; set; }
}
