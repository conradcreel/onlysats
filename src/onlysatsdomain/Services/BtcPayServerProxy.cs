using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BTCPayServer.Client;
using BTCPayServer.Client.Models;
using onlysats.domain.Models;

namespace onlysats.domain.Services
{

    /// <summary>
    /// Mostly just a wrapper around BTCPay Server to expose only the methods we need
    /// and are free to use our domain language (e.g. drop "Store", etc.)
    /// </summary>
    public interface IBitcoinPaymentProcessor
    {
        #region API Key Management

        /// <summary>
        ///
        /// </summary>
        Task<ApiKeyData> CreateAPIKey(
            CreateApiKeyRequest request,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task RevokeCurrentAPIKeyInfo(
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task RevokeAPIKey(
            string apiKey,
            CancellationToken token = default);

        #endregion

        #region Wallet Setup

        /// <summary>
        ///
        /// </summary>
        Task<OnChainPaymentMethodData> UpdateOnChainPaymentMethod(
            string accountId,
            UpdateOnChainPaymentMethodRequest request,
            CancellationToken token = default);

        /// <summary>
        /// Without persisting the xPub key, this will generate the first {amount} addresses
        /// from the derivation Scheme
        /// </summary>
        Task<OnChainPaymentMethodPreviewResultData> PreviewProposedOnChainPaymentMethodAddresses(
            string accountId,
            UpdateOnChainPaymentMethodRequest paymentMethod,
            int offset = 0,
            int amount = 10,
            CancellationToken token = default);

        /// <summary>
        /// This generates the first {amount} addresses from the xPub key (derivation scheme) for this account
        /// </summary>
        Task<OnChainPaymentMethodPreviewResultData> PreviewOnChainPaymentMethodAddresses(
                string accountId,
                int offset = 0,
                int amount = 10,
                CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<OnChainPaymentMethodData> GetOnChainPaymentMethod(
            string accountId,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task RemoveOnChainPaymentMethod(
            string accountId,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<StoreData> CreateAccount(
            CreateStoreRequest request,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<IEnumerable<StoreData>> GetAccounts(
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<StoreData> GetAccount(
            string accountId,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<StoreData> UpdateAccount(
            string accountId,
            UpdateStoreRequest request,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task RemoveAccount(
            string accountId,
            CancellationToken token = default);
        #endregion

        #region Invoices

        /// <summary>
        ///
        /// </summary>
        Task<IEnumerable<InvoiceData>> GetInvoices(
            string accountId,
            string[] orderId = null,
            InvoiceStatus[] status = null,
            DateTimeOffset? startDate = null,
            DateTimeOffset? endDate = null,
            string textSearch = null,
            bool includeArchived = false,
            int? skip = null,
            int? take = null,
            CancellationToken token = default);


        /// <summary>
        ///
        /// </summary>
        Task<InvoiceData> GetInvoice(
            string accountId,
            string invoiceId,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<InvoicePaymentMethodDataModel[]> GetInvoicePaymentMethods(
            string accountId,
            string invoiceId,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task ArchiveInvoice(
            string accountId,
            string invoiceId,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<InvoiceData> CreateInvoice(
            string accountId,
            CreateInvoiceRequest request,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<InvoiceData> UpdateInvoice(
            string accountId,
            string invoiceId,

            UpdateInvoiceRequest request, CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<InvoiceData> MarkInvoiceStatus(
            string accountId,
            string invoiceId,
            MarkInvoiceStatusRequest request,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<InvoiceData> UnarchiveInvoice(
            string accountId,
            string invoiceId,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task ActivateInvoicePaymentMethod(
            string accountId,
            string invoiceId,
            string paymentMethod,
            CancellationToken token = default);

        #endregion

        #region On-Chain

        /// <summary>
        ///
        /// </summary>
        Task<OnChainWalletOverviewData> ShowOnChainWalletOverview(
            string accountId,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<IEnumerable<OnChainWalletTransactionData>> ShowOnChainWalletTransactions(
            string accountId,
            TransactionStatus[] statusFilter = null,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<OnChainWalletTransactionData> GetOnChainWalletTransaction(
            string accountId,
            string transactionId,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<IEnumerable<OnChainWalletUTXOData>> GetOnChainWalletUTXOs(
            string accountId,
            CancellationToken token = default);

        #endregion

        #region Payment Requests

        /// <summary>
        ///
        /// </summary>
        Task<IEnumerable<PaymentRequestData>> GetPaymentRequests(
            string accountId,
            bool includeArchived = false,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<PaymentRequestData> GetPaymentRequest(
            string accountId,
            string paymentRequestId,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task ArchivePaymentRequest(
            string accountId,
            string paymentRequestId,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<PaymentRequestData> CreatePaymentRequest(
            string accountId,
            CreatePaymentRequestRequest request,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<PaymentRequestData> UpdatePaymentRequest(
            string accountId,
            string paymentRequestId,
            UpdatePaymentRequestRequest request,
            CancellationToken token = default);

        #endregion

        #region Pull Payments

        ///<summary>
        ///
        ///</summary>
        Task<PullPaymentData> CreatePullPayment(
            string accountId,
            CreatePullPaymentRequest request,
            CancellationToken cancellationToken = default);

        ///<summary>
        ///
        ///</summary>
        Task<PullPaymentData> GetPullPayment(
            string pullPaymentId,
            CancellationToken cancellationToken = default);

        ///<summary>
        ///
        ///</summary>
        Task<PullPaymentData[]> GetPullPayments(
            string accountId,
            bool includeArchived = false,
            CancellationToken cancellationToken = default);

        ///<summary>
        ///
        ///</summary>
        Task ArchivePullPayment(
            string accountId,
            string pullPaymentId,
            CancellationToken cancellationToken = default);

        ///<summary>
        ///
        ///</summary>
        Task<PayoutData[]> GetPayouts(
            string pullPaymentId,
            bool includeCancelled = false,
            CancellationToken cancellationToken = default);

        ///<summary>
        ///
        ///</summary>
        Task<PayoutData> CreatePayout(
            string pullPaymentId,
            CreatePayoutRequest payoutRequest,
            CancellationToken cancellationToken = default);

        ///<summary>
        ///
        ///</summary>
        Task CancelPayout(
            string accountId,
            string payoutId,
            CancellationToken cancellationToken = default);

        ///<summary>
        ///
        ///</summary>
        Task<PayoutData> ApprovePayout(
            string accountId,
            string payoutId,
            ApprovePayoutRequest request,
            CancellationToken cancellationToken = default);

        ///<summary>
        ///
        ///</summary>
        Task MarkPayoutPaid(
            string accountId,
            string payoutId,
            CancellationToken cancellationToken = default);

        #endregion

        #region Lightning

        /// <summary>
        ///
        /// </summary>
        Task<LightningInvoiceData> CreateLightningInvoice(
            string accountId,
            CreateLightningInvoiceRequest request,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<LightningInvoiceData> GetLightningInvoice(
            string accountId,
            string invoiceId,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task PayLightningInvoice(
            string accountId,
            PayLightningInvoiceRequest request,
            CancellationToken token = default);

        /// <summary>
        ///
        /// </summary>
        Task<string> GetLightningDepositAddress(
            string accountId,
            CancellationToken token = default);
        // TODO: Others?
        #endregion

    }

    public class BtcPayServerProxy : IBitcoinPaymentProcessor
    {
        private const string BTC = nameof(BTC);
        private BTCPayServerClient _Client { get; }
        public BtcPayServerProxy(HttpClient httpClient, OnlySatsConfiguration configuration)
        {
            _Client = new BTCPayServerClient(new Uri(configuration.BtcPayUri),
                                            configuration.BtcPayAdminUser,
                                            configuration.BtcPayAdminPass,
                                            httpClient);
        }

        public async Task<OnChainPaymentMethodData> UpdateOnChainPaymentMethod(string accountId, UpdateOnChainPaymentMethodRequest request, CancellationToken token = default)
        {
            return await _Client.UpdateStoreOnChainPaymentMethod(accountId, BTC, request, token);
        }

        public async Task<StoreData> CreateAccount(CreateStoreRequest request, CancellationToken token = default)
        {
            return await _Client.CreateStore(request, token);
        }

        public async Task<IEnumerable<StoreData>> GetAccounts(CancellationToken token = default)
        {
            return await _Client.GetStores(token);
        }

        public async Task<ApiKeyData> CreateAPIKey(CreateApiKeyRequest request, CancellationToken token = default)
        {
            return await _Client.CreateAPIKey(request, token);
        }

        public async Task RevokeCurrentAPIKeyInfo(CancellationToken token = default)
        {
            await RevokeCurrentAPIKeyInfo(token);
        }

        public async Task RevokeAPIKey(string apiKey, CancellationToken token = default)
        {
            await RevokeAPIKey(apiKey, token);
        }

        public async Task<OnChainPaymentMethodPreviewResultData> PreviewProposedOnChainPaymentMethodAddresses(string accountId, UpdateOnChainPaymentMethodRequest paymentMethod, int offset = 0, int amount = 10, CancellationToken token = default)
        {
            return await _Client.PreviewProposedStoreOnChainPaymentMethodAddresses(accountId, BTC, paymentMethod, offset, amount, token);
        }

        public async Task<OnChainPaymentMethodPreviewResultData> PreviewOnChainPaymentMethodAddresses(string accountId, int offset = 0, int amount = 10, CancellationToken token = default)
        {
            return await _Client.PreviewStoreOnChainPaymentMethodAddresses(accountId, BTC, offset, amount, token);
        }

        public async Task<OnChainPaymentMethodData> GetOnChainPaymentMethod(string accountId, CancellationToken token = default)
        {
            return await _Client.GetStoreOnChainPaymentMethod(accountId, BTC, token);
        }

        public async Task RemoveOnChainPaymentMethod(string accountId, CancellationToken token = default)
        {
            await _Client.RemoveStoreOnChainPaymentMethod(accountId, BTC, token);
        }

        public async Task<StoreData> GetAccount(string accountId, CancellationToken token = default)
        {
            return await _Client.GetStore(accountId, token);
        }

        public async Task<StoreData> UpdateAccount(string accountId, UpdateStoreRequest request, CancellationToken token = default)
        {
            return await _Client.UpdateStore(accountId, request, token);
        }

        public async Task RemoveAccount(string accountId, CancellationToken token = default)
        {
            await _Client.RemoveStore(accountId, token);
        }

        public async Task<IEnumerable<InvoiceData>> GetInvoices(string accountId, string[] orderId = null, InvoiceStatus[] status = null, DateTimeOffset? startDate = null, DateTimeOffset? endDate = null, string textSearch = null, bool includeArchived = false, int? skip = null, int? take = null, CancellationToken token = default)
        {
            return await _Client.GetInvoices(accountId, orderId, status, startDate, endDate, textSearch, includeArchived, token);
        }

        public async Task<InvoiceData> GetInvoice(string accountId, string invoiceId, CancellationToken token = default)
        {
            return await _Client.GetInvoice(accountId, invoiceId, token);
        }

        public async Task<InvoicePaymentMethodDataModel[]> GetInvoicePaymentMethods(string accountId, string invoiceId, CancellationToken token = default)
        {
            return await _Client.GetInvoicePaymentMethods(accountId, invoiceId, token);
        }

        public async Task ArchiveInvoice(string accountId, string invoiceId, CancellationToken token = default)
        {
            await _Client.ArchiveInvoice(accountId, invoiceId, token);
        }

        public async Task<InvoiceData> CreateInvoice(string accountId, CreateInvoiceRequest request, CancellationToken token = default)
        {
            return await _Client.CreateInvoice(accountId, request, token);
        }

        public async Task<InvoiceData> UpdateInvoice(string accountId, string invoiceId, UpdateInvoiceRequest request, CancellationToken token = default)
        {
            return await _Client.UpdateInvoice(accountId, invoiceId, request, token);
        }

        public async Task<InvoiceData> MarkInvoiceStatus(string accountId, string invoiceId, MarkInvoiceStatusRequest request, CancellationToken token = default)
        {
            return await _Client.MarkInvoiceStatus(accountId, invoiceId, request, token);
        }

        public async Task<InvoiceData> UnarchiveInvoice(string accountId, string invoiceId, CancellationToken token = default)
        {
            return await _Client.UnarchiveInvoice(accountId, invoiceId, token);
        }

        public async Task ActivateInvoicePaymentMethod(string accountId, string invoiceId, string paymentMethod, CancellationToken token = default)
        {
            await _Client.ActivateInvoicePaymentMethod(accountId, invoiceId, paymentMethod, token);
        }

        public async Task<OnChainWalletOverviewData> ShowOnChainWalletOverview(string accountId, CancellationToken token = default)
        {
            return await _Client.ShowOnChainWalletOverview(accountId, BTC, token);
        }

        public async Task<IEnumerable<OnChainWalletTransactionData>> ShowOnChainWalletTransactions(string accountId, TransactionStatus[] statusFilter = null, CancellationToken token = default)
        {
            return await _Client.ShowOnChainWalletTransactions(accountId, BTC, statusFilter, token);
        }

        public async Task<OnChainWalletTransactionData> GetOnChainWalletTransaction(string accountId, string transactionId, CancellationToken token = default)
        {
            return await _Client.GetOnChainWalletTransaction(accountId, BTC, transactionId, token);
        }

        public async Task<IEnumerable<OnChainWalletUTXOData>> GetOnChainWalletUTXOs(string accountId, CancellationToken token = default)
        {
            return await _Client.GetOnChainWalletUTXOs(accountId, BTC, token);
        }

        public async Task<IEnumerable<PaymentRequestData>> GetPaymentRequests(string accountId, bool includeArchived = false, CancellationToken token = default)
        {
            return await _Client.GetPaymentRequests(accountId, includeArchived, token);
        }

        public async Task<PaymentRequestData> GetPaymentRequest(string accountId, string paymentRequestId, CancellationToken token = default)
        {
            return await _Client.GetPaymentRequest(accountId, paymentRequestId, token);
        }

        public async Task ArchivePaymentRequest(string accountId, string paymentRequestId, CancellationToken token = default)
        {
            await _Client.ArchivePaymentRequest(accountId, paymentRequestId, token);
        }

        public async Task<PaymentRequestData> CreatePaymentRequest(string accountId, CreatePaymentRequestRequest request, CancellationToken token = default)
        {
            return await _Client.CreatePaymentRequest(accountId, request, token);
        }

        public async Task<PaymentRequestData> UpdatePaymentRequest(string accountId, string paymentRequestId, UpdatePaymentRequestRequest request, CancellationToken token = default)
        {
            return await _Client.UpdatePaymentRequest(accountId, paymentRequestId, request, token);
        }

        public async Task<PullPaymentData> CreatePullPayment(string accountId, CreatePullPaymentRequest request, CancellationToken cancellationToken = default)
        {
            return await _Client.CreatePullPayment(accountId, request, cancellationToken);
        }

        public async Task<PullPaymentData> GetPullPayment(string pullPaymentId, CancellationToken cancellationToken = default)
        {
            return await _Client.GetPullPayment(pullPaymentId, cancellationToken);
        }

        public async Task<PullPaymentData[]> GetPullPayments(string accountId, bool includeArchived = false, CancellationToken cancellationToken = default)
        {
            return await _Client.GetPullPayments(accountId, includeArchived, cancellationToken);
        }

        public async Task ArchivePullPayment(string accountId, string pullPaymentId, CancellationToken cancellationToken = default)
        {
            await _Client.ArchivePullPayment(accountId, pullPaymentId, cancellationToken);
        }

        public async Task<PayoutData[]> GetPayouts(string pullPaymentId, bool includeCancelled = false, CancellationToken cancellationToken = default)
        {
            return await _Client.GetPayouts(pullPaymentId, includeCancelled, cancellationToken);
        }

        public async Task<PayoutData> CreatePayout(string pullPaymentId, CreatePayoutRequest payoutRequest, CancellationToken cancellationToken = default)
        {
            return await _Client.CreatePayout(pullPaymentId, payoutRequest, cancellationToken);
        }

        public async Task CancelPayout(string accountId, string payoutId, CancellationToken cancellationToken = default)
        {
            await _Client.CancelPayout(accountId, payoutId, cancellationToken);
        }

        public async Task<PayoutData> ApprovePayout(string accountId, string payoutId, ApprovePayoutRequest request, CancellationToken cancellationToken = default)
        {
            return await _Client.ApprovePayout(accountId, payoutId, request, cancellationToken);
        }

        public async Task MarkPayoutPaid(string accountId, string payoutId, CancellationToken cancellationToken = default)
        {
            await _Client.MarkPayoutPaid(accountId, payoutId, cancellationToken);
        }

        public async Task<LightningInvoiceData> CreateLightningInvoice(string accountId, CreateLightningInvoiceRequest request, CancellationToken token = default)
        {
            return await _Client.CreateLightningInvoice(accountId, BTC, request, token);
        }

        public async Task<LightningInvoiceData> GetLightningInvoice(string accountId, string invoiceId, CancellationToken token = default)
        {
            return await _Client.GetLightningInvoice(accountId, BTC, invoiceId, token);
        }

        public async Task PayLightningInvoice(string accountId, PayLightningInvoiceRequest request, CancellationToken token = default)
        {
            await _Client.PayLightningInvoice(accountId, BTC, request, token);
        }

        public async Task<string> GetLightningDepositAddress(string accountId, CancellationToken token = default)
        {
            return await _Client.GetLightningDepositAddress(accountId, BTC, token);
        }
    }
}