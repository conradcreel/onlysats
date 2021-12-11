namespace onlysats.domain.Models;

public class Patron
{
    public int Id { get; set; }
    public int UserAccountId { get; set; }
    public List<PatronAsset>? Assets { get; set; }
    public DateTime? MemberUntil { get; set; }
}
