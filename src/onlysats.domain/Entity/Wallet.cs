namespace onlysats.domain.Entity;

/// <summary>
/// Stores reference data from BTCPayServer after importing their 
/// xPubKey. We don't store the xPubkey to limit privacy implications
/// </summary>
public class Wallet
{
    /// <summary>
    ///
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int UserAccountId { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? Nickname { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? BtcPayServerStoreId { get; set; }
}