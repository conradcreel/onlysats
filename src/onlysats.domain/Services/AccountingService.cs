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
    private readonly IBitcoinPaymentProcessor _BitcoinPaymentProcessor;
    private readonly IPaymentRepository _PaymentRepository;
    private readonly IMessagePublisher _MessagePublisher;

    public AccountingService(IPaymentRepository paymentRepository,
                            IBitcoinPaymentProcessor bitcoinPaymentProcessor,
                            IMessagePublisher messagePublisher)
    {
        _PaymentRepository = paymentRepository;
        _BitcoinPaymentProcessor = bitcoinPaymentProcessor;
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
