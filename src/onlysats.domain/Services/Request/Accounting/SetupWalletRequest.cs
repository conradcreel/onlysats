namespace onlysats.domain.Services.Request.Accounting;

public class SetupWalletRequest : RequestBase
{
    public int UserAccountId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string XPubKey { get; set; } = string.Empty;

    public override bool IsValid()
    {
        // TODO: Additional validation?
        return UserAccountId > 0 && 
                !string.IsNullOrWhiteSpace(Username) && 
                !string.IsNullOrWhiteSpace(XPubKey);
    }
}