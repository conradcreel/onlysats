namespace onlysats.domain.Models;

public class OnlySatsConfiguration
{
    public string SqlConnectionString { get; set; } = string.Empty;
    public string BtcPayUri { get; set; } = string.Empty;
    public string? BtcPayAdminUser { get; set; }
    public string? BtcPayAdminPass { get; set; }
}
