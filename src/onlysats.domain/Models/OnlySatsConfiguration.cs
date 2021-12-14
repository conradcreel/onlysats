namespace onlysats.domain.Models;

public class OnlySatsConfiguration
{
    public string SqlConnectionString { get; set; } = string.Empty;
    public string PubSubName { get; set; } = string.Empty;

    // TODO: Blob properties
    public string BtcPayUri { get; set; } = string.Empty;
    public string? BtcPayAdminUser { get; set; }
    public string? BtcPayAdminPass { get; set; }
}
