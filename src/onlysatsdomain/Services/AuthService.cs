using System;
using System.Threading.Tasks;
using onlysats.domain.Constants;
using onlysats.domain.Services.Repositories;
using onlysats.domain.Services.Request.Chat;
using onlysats.domain.Services.Request.Auth;
using onlysats.domain.Services.Response;
using onlysats.domain.Services.Response.Auth;
using onlysats.domain.Entity;

namespace onlysats.domain.Services
{

    /// <summary>
    /// Manages Authentication with the OnlySats and Synapse backend
    /// </summary>
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }

    #region Implementation

    public class AuthService : IAuthService
    {
        private readonly IUserAccountRepository _UserAccountRepository;
        private readonly IChatService _ChatService;
        private readonly IMessagePublisher _MessagePublisher;

        public AuthService(IUserAccountRepository userAccountRepository,
                            IChatService chatService,
                            IMessagePublisher messagePublisher)
        {
            _UserAccountRepository = userAccountRepository;
            _ChatService = chatService;
            _MessagePublisher = messagePublisher;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new LoginResponse()
                            .BadRequest(CErrorMessage.LOGIN_BAD_REQUEST);
            }

            SynapseLoginRequest synapseLoginRequest;
            UserAccount userAccount;

            if (!request.AdminRequest)
            {
                userAccount = await _UserAccountRepository.GetUserAccount(request.Username, HashService.SHA256(request.Password));

                if (userAccount == null || userAccount.Id <= 0)
                {
                    return new LoginResponse()
                                .NotFound();
                }

                synapseLoginRequest = new SynapseLoginRequest
                {
                    User = userAccount.Username,
                    Password = userAccount.ChatPassword
                };
            }
            else
            {
                userAccount = new UserAccount
                {
                    Id = 0,
                    Username = $"admin:{request.Username}",
                    Role = Enums.EUserRole.ADMIN
                };

                synapseLoginRequest = new SynapseLoginRequest
                {
                    User = request.Username,
                    Password = request.Password
                };
            }

            var synapseLogin = await _ChatService.Login(synapseLoginRequest).ConfigureAwait(continueOnCapturedContext: false);

            if (!synapseLogin.ResponseDetails.IsSuccess)
            {
                return new LoginResponse
                {
                    ResponseDetails = synapseLogin.ResponseDetails
                };
            }

            return new LoginResponse
            {
                UserAccountId = userAccount.Id,
                Username = userAccount.Username,
                Role = userAccount.Role,
                ChatAccessToken = synapseLogin.AccessToken
            }.OK();
        }
    }

    #endregion
}