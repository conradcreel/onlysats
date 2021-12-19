namespace onlysats.domain.Services.Response.Accounting;

public class SetupWalletResponse : ResponseBase
{
    public string? BtcPaymentProcessorId { get; set; }
    public string? Nickname { get; set; }
    public int WalletId { get; set; }
    public int UserAccountId { get; set; }
}