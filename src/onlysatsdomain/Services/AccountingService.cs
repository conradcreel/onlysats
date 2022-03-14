using System;
using System.Linq;
using System.Threading.Tasks;
using BTCPayServer.Client.Models;
using onlysats.domain.Constants;
using onlysats.domain.Entity;
using onlysats.domain.Events;
using onlysats.domain.Services.Repositories;
using onlysats.domain.Services.Request.Accounting;
using onlysats.domain.Services.Response;
using onlysats.domain.Services.Response.Accounting;

namespace onlysats.domain.Services
{

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

            var wallets = await _PaymentRepository.GetWallets(request.UserAccountId);
            var wallet = wallets?.FirstOrDefault();
            var btcPayAccountId = string.Empty;

            if (wallet == null)
            {
                var btcPayAccount = await _BitcoinPaymentProcessor.CreateAccount(new CreateStoreRequest
                {
                    Name = request.Username
                });

                if (btcPayAccount == null)
                {
                    return new SetupWalletResponse()
                                .ServerError(CErrorMessage.SETUP_WALLET_CANNOT_CREATE_ACCOUNT);
                }

                btcPayAccountId = btcPayAccount.Id;

                wallet = await _PaymentRepository.UpsertWallet(new Wallet
                {
                    UserAccountId = request.UserAccountId,
                    BtcPayServerAccountId = btcPayAccountId,
                    Nickname = $"{request.Username}_wallet"
                });

                if (wallet == null)
                {
                    return new SetupWalletResponse()
                                .ServerError(CErrorMessage.SETUP_WALLET_CANNOT_CREATE_WALLET);
                }
            }
            else
            {
                if (wallet.BtcPayServerAccountId == null)
                {
                    throw new Exception("TODO: Shouldn't happen");
                }

                btcPayAccountId = wallet.BtcPayServerAccountId;
            }

            var paymentMethod = await _BitcoinPaymentProcessor.UpdateOnChainPaymentMethod(btcPayAccountId, new UpdateOnChainPaymentMethodRequest
            {
                Enabled = true,
                DerivationScheme = request.XPubKey,
                Label = wallet.Nickname
            });

            if (paymentMethod == null)
            {
                return new SetupWalletResponse()
                            .ServerError(CErrorMessage.SETUP_WALLET_CANNOT_UPDATE_WALLET);
            }

            var domainEvent = new WalletUpdatedEvent
            {
                Nickname = wallet.Nickname,
                BtcPaymentProcessorId = wallet.BtcPayServerAccountId,
                UserAccountId = wallet.UserAccountId,
                WalletId = wallet.Id
            };

            await _MessagePublisher.PublishEvent(domainEvent.Topic, domainEvent);

            return new SetupWalletResponse
            {
                WalletId = wallet.Id,
                UserAccountId = wallet.UserAccountId,
                Nickname = wallet.Nickname,
                BtcPaymentProcessorId = wallet.BtcPayServerAccountId
            }.OK();
        }
    }

    #endregion
}