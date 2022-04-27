using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using BTCPayServer.Client;
using BTCPayServer.Client.Models;
using onlysats.domain.Models;
using onlysats.domain.Models.BtcPayServer;

namespace onlysats.domain.Services
{

    /// <summary>
    /// Mostly just a wrapper around BTCPay Server to expose only the methods we need
    /// and are free to use our domain language (e.g. drop "Store", etc.)
    /// </summary>
    public interface IBitcoinPaymentProcessor
    {
        #region Lightning

        /// <summary>
        ///
        /// </summary>
        Task<LightningInvoiceModel> CreateLightningInvoice(CreateLightningPaymentRequest request);
        #endregion

    }

    public class BtcPayServerProxy : IBitcoinPaymentProcessor
    {
        private const string BTC = nameof(BTC);
        private BTCPayServerClient _Client { get; }
        private readonly HttpClient _HttpClient;
        private readonly OnlySatsConfiguration _Configuration;

        public BtcPayServerProxy(HttpClient httpClient, OnlySatsConfiguration configuration)
        {
            // TODO: base64Encode(user:pass)
            _HttpClient = httpClient;
            _Configuration = configuration;

            _Client = new BTCPayServerClient(new Uri(configuration.BtcPayUri),
                                            configuration.BtcPayAdminUser,
                                            configuration.BtcPayAdminPass);
        }

        private JsonSerializerOptions SerializerOptions => new JsonSerializerOptions
        {
            IgnoreReadOnlyFields = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            PropertyNameCaseInsensitive = true
        };

        // TODO: Cleanup
        // Note: it's a weird workflow, I need to create a generic invoice and then call the get payment methods endpoint
        // after creation to get the BOLT11 URL
        public async Task<LightningInvoiceModel> CreateLightningInvoice(CreateLightningPaymentRequest request)
        {
            //var response = await _Client.CreateLightningInvoice(accountId, BTC, request, token);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_Configuration.BtcPayUri}/api/v1/stores/{_Configuration.BtcPayStoreId}/invoices");
            httpRequestMessage.Headers.Add("Accept", "application/json");

            httpRequestMessage.Headers.Add("Authorization", $"Basic {"dGVzdG5ldEBidGMubG9jYWw6NWEyNWE4YmFlMTg5NDM3ZmIyNjUyZjRmNTljNThlNWY="}");

            var data = new
            {
                metdata = new
                {
                    orderId = request.Id,
                    itemDesc = request.Description
                },
                checkout = new
                {
                    paymentMethods = new string[] { "BTC-LightningNetwork" },
                    expirationMinutes = request.ExpiryMinutes
                },
                amount = request.Satoshis.ToString(),
                currency = "SATS"
            };

            var requestBody = JsonSerializer.Serialize(data, SerializerOptions);

            httpRequestMessage.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var httpResponse = await _HttpClient.SendAsync(httpRequestMessage).ConfigureAwait(continueOnCapturedContext: false);

            var responseBody = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                // TODO: come back
                return null;
            }

            var invoiceModel = JsonSerializer.Deserialize<LightningInvoiceModel>(responseBody);

            var paymentMethod = await GetPaymentMethod(invoiceModel.Id);

            invoiceModel.BOLT11 = paymentMethod.Destination;

            return invoiceModel;
        }

        private async Task<InvoicePaymentMethodModel> GetPaymentMethod(string invoiceId)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_Configuration.BtcPayUri}/api/v1/stores/{_Configuration.BtcPayStoreId}/invoices/{invoiceId}/payment-methods");
            httpRequestMessage.Headers.Add("Accept", "application/json");

            httpRequestMessage.Headers.Add("Authorization", $"Basic {"dGVzdG5ldEBidGMubG9jYWw6NWEyNWE4YmFlMTg5NDM3ZmIyNjUyZjRmNTljNThlNWY="}");

            var httpResponse = await _HttpClient.SendAsync(httpRequestMessage).ConfigureAwait(continueOnCapturedContext: false);

            var responseBody = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                // TODO: come back
                return null;
            }

            var response = JsonSerializer.Deserialize<List<InvoicePaymentMethodModel>>(responseBody);

            return response.FirstOrDefault();
        }
    }
}