using BTCPayServer.Client;
using onlysats.domain.Models;

namespace onlysats.domain.Services;

public class BtcPayServerProxy
{
    public BTCPayServerClient Client { get; }
    public BtcPayServerProxy(HttpClient httpClient, OnlySatsConfiguration configuration)
    {
        Client = new BTCPayServerClient(new Uri(configuration.BtcPayUri),
                                        configuration.BtcPayAdminUser,
                                        configuration.BtcPayAdminPass,
                                        httpClient);
    }
}
