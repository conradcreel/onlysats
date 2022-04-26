using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using onlysats.domain.Entity;
using onlysats.domain.Models;
using onlysats.domain.Services.Repositories;
using onlysats.domain.Services.Request;
using onlysats.domain.Services.Request.Chat;
using onlysats.domain.Services.Response;
using onlysats.domain.Services.Response.Chat;

namespace onlysats.domain.Services
{
    /// <summary>
    /// Provides communication with a backend chat server to manage room and chat state
    /// Provides temporary storage of certain types of messages before publishing to 
    /// the backend chat server. For example: A creator may attach a Lightning invoice
    /// to a message which must be paid prior to displaying. In such cases, this service 
    /// would keep track of unpaid invoices and would not push the message to the chat 
    /// server until the invoice was paid
    /// </summary>
    public interface IChatService
    {
        /// <summary>
        /// Creates the user in the Chat backend. Requires an Admin access token.
        /// Usernames and passwords are dynamically generated GUIDs. They are not
        /// meant to be user-friendly or ever exposed to the user
        /// </summary>
        Task<CreateUserResponse> CreateUser(CreateUserRequest request);

        /// <summary>
        /// Given an existing username and password, we can retrieve the an access
        /// token to use against the chat server backend. This username and password
        /// is not visible to the user, it's managed behind the scenes.
        /// </summary>
        Task<SynapseLoginResponse> Login(SynapseLoginRequest request);

        /// <summary>
        /// Creates a private room for a (creator, patron) pair. This is how DMs will be implemented.
        /// This will create a room in synapse and then add each user to that room
        /// </summary>
        Task<CreateRoomResponse> CreateRoom(CreateRoomRequest request);

        /// <summary>
        /// Given a user's access token, retrieve a list of the rooms they have joined.
        /// For creators this will consist of every DM they've engaged with.
        /// For patrons this will usually only be one (their DM with the Creator)
        /// 
        /// If AdminRequest = true, it will call the admin API /rooms endpoint which 
        /// will return all rooms
        ///
        /// Note: Currently investigating how we may use Synapse as the backend for
        /// the timeline. Might be more trouble than its worth
        /// </summary>
        Task<GetRoomListResponse> GetRoomList(GetRoomListRequest request);

        /// <summary>
        /// Queues a message which will be released to the Chat Server upon successful
        /// completion of some release conditions. For example, a Lightning invoice
        /// can be attached to a particular message. It'll show up in the chat as 
        /// locked since the chat front end is a combination of the Synapse backend
        /// and queued messages. Once the condition is completed, e.g. Lightning invoice
        /// paid, it will be released to the chat server and show up in the chat.
        /// </summary>
        Task<QueueMessageResponse> QueueMessage(QueueMessageRequest request);

        /// <summary>
        /// Sends a message to the chat server
        /// </summary>
        Task<SendMessageResponse> SendMessage(SendMessageRequest request);

        /// <summary>
        /// Retrieve all events for a particular room. Most importantly, the messages
        /// for that room
        /// </summary>
        Task<GetRoomEventsResponse> GetRoomEvents(GetRoomEventsRequest request);

        /// <summary>
        /// Gets a single room event by Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetRoomEventResponse> GetRoomEvent(GetRoomEventRequest request);

        /// <summary>
        /// Gets the Room details to display once the chat has been opened
        /// </summary>
        Task<GetRoomDetailsResponse> GetRoomDetails(GetRoomDetailsRequest request);


        /// <summary>
        /// NOT NEEDED FOR THIS USE-CASE
        /// Gets an initial snapshot of the server's state and then can be used to retrieve 
        /// subsequent deltas
        ///
        /// From Synapse/matrix documentation:
        /// Synchronise the client's state with the latest state on the server. Clients use 
        /// this API when they first log in to get an initial snapshot of the state on the 
        /// server, and then continue to call this API to get incremental deltas to the state, 
        /// and to receive new messages.
        /// </summary>
        Task<SyncStateResponse> SyncState(SyncStateRequest request);
    }

    #region Implementation

    public class SynapseChatService : IChatService
    {
        private const string _ContentType = "application/json";
        private const string _Accept = "application/json";
        private readonly HttpClient _HttpClient;
        private readonly OnlySatsConfiguration _Configuration;
        private readonly IChatRepository _ChatRepository;
        private readonly IBitcoinPaymentProcessor _BitcoinPaymentProcessor;

        public SynapseChatService(IChatRepository chatRepository,
                                  IBitcoinPaymentProcessor bitcoinPaymentProcessor,
                                  OnlySatsConfiguration configuration,
                                  HttpClient client)
        {
            _ChatRepository = chatRepository;
            _BitcoinPaymentProcessor = bitcoinPaymentProcessor;
            _Configuration = configuration;
            _HttpClient = client;
        }

        private JsonSerializerOptions SerializerOptions => new JsonSerializerOptions
        {
            IgnoreReadOnlyFields = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        protected async Task<HttpResponseMessage>
            SendPostAsync<TRequest>(TRequest data) where TRequest : SynapseRequestBase
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_Configuration.SynapseUri}/{data.GenerateUrl()}");
            httpRequestMessage.Headers.Add("Accept", _Accept);

            if (!string.IsNullOrWhiteSpace(data?.UserContext?.ChatAccessToken))//.AccessToken))
            {
                httpRequestMessage.Headers.Add("Authorization", $"Bearer {data.UserContext.ChatAccessToken}");
            }

            var requestBody = JsonSerializer.Serialize(data, SerializerOptions);

            httpRequestMessage.Content = new StringContent(requestBody, Encoding.UTF8, _ContentType);

            return await _HttpClient.SendAsync(httpRequestMessage).ConfigureAwait(continueOnCapturedContext: false);
        }

        protected async Task<HttpResponseMessage>
            SendPutAsync<TRequest>(TRequest data) where TRequest : SynapseRequestBase
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, $"{_Configuration.SynapseUri}/{data.GenerateUrl()}");
            httpRequestMessage.Headers.Add("Accept", _Accept);

            if (!string.IsNullOrWhiteSpace(data?.UserContext?.ChatAccessToken))//.AccessToken))
            {
                httpRequestMessage.Headers.Add("Authorization", $"Bearer {data.UserContext.ChatAccessToken}");
            }

            var requestBody = JsonSerializer.Serialize(data, SerializerOptions);

            httpRequestMessage.Content = new StringContent(requestBody, Encoding.UTF8, _ContentType);

            return await _HttpClient.SendAsync(httpRequestMessage).ConfigureAwait(continueOnCapturedContext: false);
        }

        protected async Task<HttpResponseMessage>
            SendGetAsync<TRequest>(TRequest data) where TRequest : SynapseRequestBase
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_Configuration.SynapseUri}/{data.GenerateUrl()}");
            httpRequestMessage.Headers.Add("Accept", _Accept);

            if (!string.IsNullOrWhiteSpace(data?.UserContext?.ChatAccessToken))//.AccessToken))
            {
                httpRequestMessage.Headers.Add("Authorization", $"Bearer {data.UserContext.ChatAccessToken}");
            }

            return await _HttpClient.SendAsync(httpRequestMessage).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<CreateRoomResponse> CreateRoom(CreateRoomRequest request)
        {
            var response = await SendPutAsync<CreateRoomRequest>(request)
                                    .ConfigureAwait(continueOnCapturedContext: false);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new CreateRoomResponse().NotFound();

                    case System.Net.HttpStatusCode.BadRequest:
                        return new CreateRoomResponse().BadRequest(errorBody);

                    case System.Net.HttpStatusCode.Unauthorized:
                        return new CreateRoomResponse().Unauthorized();

                    case System.Net.HttpStatusCode.Forbidden:
                        return new CreateRoomResponse().Forbidden();

                    default:
                        return new CreateRoomResponse().ServerError(errorBody);
                }
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var dto = JsonSerializer.Deserialize<CreateRoomResponse>(responseBody);

            return dto.OK();
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            var response = await SendPutAsync<CreateUserRequest>(request)
                                    .ConfigureAwait(continueOnCapturedContext: false);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new CreateUserResponse().NotFound();

                    case System.Net.HttpStatusCode.BadRequest:
                        return new CreateUserResponse().BadRequest(errorBody);

                    case System.Net.HttpStatusCode.Unauthorized:
                        return new CreateUserResponse().Unauthorized();

                    case System.Net.HttpStatusCode.Forbidden:
                        return new CreateUserResponse().Forbidden();

                    default:
                        return new CreateUserResponse().ServerError(errorBody);
                }
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var dto = JsonSerializer.Deserialize<CreateUserResponse>(responseBody);

            return dto.OK();
        }

        public async Task<GetRoomEventResponse> GetRoomEvent(GetRoomEventRequest request)
        {
            var response = await SendGetAsync<GetRoomEventRequest>(request)
                                    .ConfigureAwait(continueOnCapturedContext: false);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new GetRoomEventResponse().NotFound();

                    case System.Net.HttpStatusCode.BadRequest:
                        return new GetRoomEventResponse().BadRequest(errorBody);

                    case System.Net.HttpStatusCode.Unauthorized:
                        return new GetRoomEventResponse().Unauthorized();

                    case System.Net.HttpStatusCode.Forbidden:
                        return new GetRoomEventResponse().Forbidden();

                    default:
                        return new GetRoomEventResponse().ServerError(errorBody);
                }
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var dto = JsonSerializer.Deserialize<GetRoomEventResponse>(responseBody);

            return dto.OK();
        }

        public async Task<GetRoomEventsResponse> GetRoomEvents(GetRoomEventsRequest request)
        {
            var response = await SendGetAsync<GetRoomEventsRequest>(request)
                                    .ConfigureAwait(continueOnCapturedContext: false);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new GetRoomEventsResponse().NotFound();

                    case System.Net.HttpStatusCode.BadRequest:
                        return new GetRoomEventsResponse().BadRequest(errorBody);

                    case System.Net.HttpStatusCode.Unauthorized:
                        return new GetRoomEventsResponse().Unauthorized();

                    case System.Net.HttpStatusCode.Forbidden:
                        return new GetRoomEventsResponse().Forbidden();

                    default:
                        return new GetRoomEventsResponse().ServerError(errorBody);
                }
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var dto = JsonSerializer.Deserialize<GetRoomEventsResponse>(responseBody);
            dto.RoomEvents.Reverse(); // so newest is last

            return dto.OK();
        }

        public async Task<GetRoomListResponse> GetRoomList(GetRoomListRequest request)
        {
            var response = await SendGetAsync<GetRoomListRequest>(request)
                                    .ConfigureAwait(continueOnCapturedContext: false);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new GetRoomListResponse().NotFound();

                    case System.Net.HttpStatusCode.BadRequest:
                        return new GetRoomListResponse().BadRequest(errorBody);

                    case System.Net.HttpStatusCode.Unauthorized:
                        return new GetRoomListResponse().Unauthorized();

                    case System.Net.HttpStatusCode.Forbidden:
                        return new GetRoomListResponse().Forbidden();

                    default:
                        return new GetRoomListResponse().ServerError(errorBody);
                }
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var dto = JsonSerializer.Deserialize<GetRoomListResponse>(responseBody);

            return dto.OK();
        }

        public async Task<SynapseLoginResponse> Login(SynapseLoginRequest request)
        {
            var response = await SendPostAsync<SynapseLoginRequest>(request)
                                    .ConfigureAwait(continueOnCapturedContext: false);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new SynapseLoginResponse().NotFound();

                    case System.Net.HttpStatusCode.BadRequest:
                        return new SynapseLoginResponse().BadRequest(errorBody);

                    case System.Net.HttpStatusCode.Unauthorized:
                        return new SynapseLoginResponse().Unauthorized();

                    case System.Net.HttpStatusCode.Forbidden:
                        return new SynapseLoginResponse().Forbidden();

                    default:
                        return new SynapseLoginResponse().ServerError(errorBody);
                }
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var dto = JsonSerializer.Deserialize<SynapseLoginResponse>(responseBody);

            return dto.OK();
        }

        public async Task<QueueMessageResponse> QueueMessage(QueueMessageRequest request)
        {
            if (!request.PaymentRequired)
            {
                var chatServerResponse = await SendMessage(new SendMessageRequest
                {
                    RoomId = request.RoomId,
                    Body = request.MessageContent,
                    FormattedBody = request.MessageContent,
                    AccessToken = request.SynapseAccessToken
                }).ConfigureAwait(continueOnCapturedContext: false);

                return new QueueMessageResponse
                {
                    ResponseDetails = chatServerResponse.ResponseDetails
                };
            }

            var milliSats = 1000 * request.CostInSatoshis;

            var invoice = await _BitcoinPaymentProcessor.CreateLightningInvoice(_Configuration.BtcPayStoreId, new BTCPayServer.Client.Models.CreateLightningInvoiceRequest
            {
                Amount = milliSats.ToString(),
                Description = request.Description,
                Expiry = new TimeSpan(0, 60, 0),
                PrivateRouteHints = true
            });

            if (string.IsNullOrWhiteSpace(invoice?.Id))
            {
                // TODO: Come back to this
                throw new Exception("Could not create Lightning invoice");
            }

            var messageContent = string.Empty; // TODO: Construct the message content from the Package selected for this paid message

            // Since a payment is required, the Sender is always going to be the Creator
            // A Patron doesn't have the option to make a message require payment
            var msg = await _ChatRepository.QueueMessage(new QueuedMessage
            {
                CreatorId = request.SenderUserId,
                RoomId = request.RoomId,
                MessageContent = messageContent,
                InvoiceId = invoice.Id,
                BOLT11 = invoice.BOLT11,
                SynapseAccessToken = request.SynapseAccessToken
            }).ConfigureAwait(continueOnCapturedContext: false);

            return new QueueMessageResponse
            {
                QueuedMessageId = msg.Id,
                InvoiceId = invoice.Id,
                BOLT11 = invoice.BOLT11
            }.OK();
        }

        public async Task<SendMessageResponse> SendMessage(SendMessageRequest request)
        {
            var response = await SendPutAsync<SendMessageRequest>(request)
                                    .ConfigureAwait(continueOnCapturedContext: false);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new SendMessageResponse().NotFound();

                    case System.Net.HttpStatusCode.BadRequest:
                        return new SendMessageResponse().BadRequest(errorBody);

                    case System.Net.HttpStatusCode.Unauthorized:
                        return new SendMessageResponse().Unauthorized();

                    case System.Net.HttpStatusCode.Forbidden:
                        return new SendMessageResponse().Forbidden();

                    default:
                        return new SendMessageResponse().ServerError(errorBody);
                }
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var dto = JsonSerializer.Deserialize<SendMessageResponse>(responseBody);

            return dto.OK();
        }

        public async Task<SyncStateResponse> SyncState(SyncStateRequest request)
        {
            var response = await SendGetAsync<SyncStateRequest>(request)
                                    .ConfigureAwait(continueOnCapturedContext: false);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new SyncStateResponse().NotFound();

                    case System.Net.HttpStatusCode.BadRequest:
                        return new SyncStateResponse().BadRequest(errorBody);

                    case System.Net.HttpStatusCode.Unauthorized:
                        return new SyncStateResponse().Unauthorized();

                    case System.Net.HttpStatusCode.Forbidden:
                        return new SyncStateResponse().Forbidden();

                    default:
                        return new SyncStateResponse().ServerError(errorBody);
                }
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var dto = JsonSerializer.Deserialize<SyncStateResponse>(responseBody);

            return dto.OK();
        }

        public async Task<GetRoomDetailsResponse> GetRoomDetails(GetRoomDetailsRequest request)
        {
            var response = await SendGetAsync<GetRoomDetailsRequest>(request)
                                .ConfigureAwait(continueOnCapturedContext: false);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new GetRoomDetailsResponse().NotFound();

                    case System.Net.HttpStatusCode.BadRequest:
                        return new GetRoomDetailsResponse().BadRequest(errorBody);

                    case System.Net.HttpStatusCode.Unauthorized:
                        return new GetRoomDetailsResponse().Unauthorized();

                    case System.Net.HttpStatusCode.Forbidden:
                        return new GetRoomDetailsResponse().Forbidden();

                    default:
                        return new GetRoomDetailsResponse().ServerError(errorBody);
                }
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var dto = JsonSerializer.Deserialize<GetRoomDetailsResponse>(responseBody);

            return dto.OK();
        }
    }

    #endregion
}