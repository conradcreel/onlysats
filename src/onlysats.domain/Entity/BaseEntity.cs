namespace onlysats.domain.Entity;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime DateAddedUtc { get; set; }
    public DateTime DateUpdatedUtc { get; set; }
}