using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BTCPayServer.Client.Models;
using Microsoft.AspNetCore.SignalR;
using onlysats.domain.Constants;
using onlysats.domain.Entity;
using onlysats.domain.Events;
using onlysats.domain.Services.Repositories;
using onlysats.domain.Services.Request.Accounting;
using onlysats.domain.Services.Request.Chat;
using onlysats.domain.Services.Response;
using onlysats.domain.Services.Response.Accounting;
using onlysats.domain.Hubs;

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
        /// TODO: Delete this. Assume BTC PayServer is configured and setup properly.
        /// </summary>
        Task<SetupWalletResponse> SetupWallet(SetupWalletRequest request);

        /*
        /// <summary>
        /// Handles the InvoiceReceivedPayment webhook by updating the status of the queued message
        /// </summary>
        Task<InvoiceReceivedPaymentResponse> HandleInvoiceReceivedPayment(InvoiceReceivedPaymentRequest request);

        /// <summary>
        /// Handles the InvoiceProcessing webhook by updating the status of the queued message
        /// </summary>
        Task<InvoiceProcessingResponse> HandleInvoiceProcessing(InvoiceProcessingRequest request);

        */

        /// <summary>
        /// Handles the InvoiceSettled webhook. At this time, the payment has been completed and the queued 
        /// message can be released to Synapse and displayed to the Patron
        /// </summary>
        Task<InvoiceSettledResponse> HandleInvoiceSettled(InvoiceSettledRequest request);
    }

    #region Implementation

    public class AccountingService : IAccountingService
    {
        private readonly IBitcoinPaymentProcessor _BitcoinPaymentProcessor;
        private readonly IPaymentRepository _PaymentRepository;
        private readonly IChatRepository _ChatRepository;
        private readonly IChatService _ChatService;
        private readonly IMessagePublisher _MessagePublisher;
        private readonly IHubContext<ChatHub> _ChatHubContext;


        public AccountingService(IPaymentRepository paymentRepository,
                                IBitcoinPaymentProcessor bitcoinPaymentProcessor,
                                IChatRepository chatRepository,
                                IChatService chatService,
                                IMessagePublisher messagePublisher,
                                IHubContext<ChatHub> chatHubContext)
        {
            _PaymentRepository = paymentRepository;
            _BitcoinPaymentProcessor = bitcoinPaymentProcessor;
            _ChatRepository = chatRepository;
            _ChatService = chatService;
            _MessagePublisher = messagePublisher;
            _ChatHubContext = chatHubContext;
        }

        public async Task<InvoiceSettledResponse> HandleInvoiceSettled(InvoiceSettledRequest request)
        {
            var queuedMessage = await _ChatRepository.GetQueuedMessageByInvoice(request.InvoiceId)
                                        .ConfigureAwait(continueOnCapturedContext: false);

            if (queuedMessage == null)
            {
                // TODO: come back to this
                return new InvoiceSettledResponse().NotFound();
            }

            var message = $"Thank you for paying Invoice {request.InvoiceId}";
            var sendMessageRequest = new SendMessageRequest
            {
                RoomId = queuedMessage.RoomId,
                UserContext = new Request.AuthenticatedUserContext
                {
                    ChatAccessToken = queuedMessage.SynapseAccessToken
                },
                Body = message,
                FormattedBody = message
            };

            var sendMessageResponse = await _ChatService.SendMessage(sendMessageRequest)
                                            .ConfigureAwait(continueOnCapturedContext: false);

            var getRoomEventRequest = new GetRoomEventRequest
            {
                RoomId = queuedMessage.RoomId,
                EventId = sendMessageResponse.EventId,
                UserContext = new Request.AuthenticatedUserContext
                {
                    ChatAccessToken = queuedMessage.SynapseAccessToken
                }
            };

            var getRoomEventResponse = await _ChatService.GetRoomEvent(getRoomEventRequest)
                                            .ConfigureAwait(continueOnCapturedContext: false);

            // TODO: This would use groups
            await _ChatHubContext.Clients.All.SendAsync("ReceiveMessage", getRoomEventResponse.Sender, message, getRoomEventResponse.OriginServerTimestamp, getRoomEventResponse.EventId);
            
            return new InvoiceSettledResponse
            {

            }.OK();
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