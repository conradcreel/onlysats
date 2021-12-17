namespace onlysats.domain.Entity;

/// <summary>
/// Stores reference data from BTCPayServer after importing their 
/// xPubKey. We don't store the xPubkey to limit privacy implications
/// </summary>
public class Wallet : BaseEntity
{
    /// <summary>
    /// Reference to the UserAccount that owns this Wallet
    /// </summary>
    public int UserAccountId { get; set; }

    /// <summary>
    /// The name given to the Wallet (BTCPay Server:Payment Method name)
    /// </summary>
    public string? Nickname { get; set; }

    /// <summary>
    /// The StoreId in BTCPay Server. From the StoreId and CryptoCode (fixed "BTC")
    /// you can retrieve the Payment Method (xPubKey, addresses, etc.)
    /// </summary>
    public string? BtcPayServerAccountId { get; set; }
}