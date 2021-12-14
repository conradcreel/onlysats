using Dapr.Client;
using onlysats.domain.Constants;
using onlysats.domain.Services.Repositories;
using onlysats.domain.Services.Request.Accounting;
using onlysats.domain.Services.Response;
using onlysats.domain.Services.Response.Accounting;

namespace onlysats.domain.Services;

/// <summary>
/// Handles all financial operations such as wallet setup, payments, invoices, etc.
/// </summary>
public interface IAccountingService
{
    /// <summary>
    /// Sets up a Store and Wallet in BTCPay Server
    /// Updates the Wallet state in OnlySats
    /// </summary>
    Task<SetupWalletResponse> SetupWallet(SetupWalletRequest request);
}

#region Implementation

public class AccountingService : IAccountingService
{
    private readonly BtcPayServerProxy _BtcPayProxy;
    private readonly IPaymentRepository _PaymentRepository;
    private readonly MessagePublisherProxy _MessagePublisher;

    public AccountingService(IPaymentRepository paymentRepository, 
                            BtcPayServerProxy btcPayProxy, 
                            MessagePublisherProxy messagePublisher)
    {
        _PaymentRepository = paymentRepository;
        _BtcPayProxy = btcPayProxy;
        _MessagePublisher = messagePublisher;
    }

    public async Task<SetupWalletResponse> SetupWallet(SetupWalletRequest request)
    {
        if (request == null || !request.IsValid())
        {
            return new SetupWalletResponse()
                        .BadRequest(CErrorMessage.SETUP_WALLET_BAD_REQUEST);
        }

        await Task.Delay(1); // TODO: remove

        throw new NotImplementedException();
    }
}

#endregion
