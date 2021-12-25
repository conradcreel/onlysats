namespace onlysats.domain.Services.Request.ContentManagement;

public class GetVaultsRequest : RequestBase
{
    public int CreatorId { get; set; }
    public int? VaultId { get; set; }

    public int Top { get; set; }
    public int Skip { get; set; }
    public override bool IsValid()
    {
        return CreatorId > 0;
    }
}