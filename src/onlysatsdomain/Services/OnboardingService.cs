using System.Security.Cryptography;
using System.Threading.Tasks;
using onlysats.domain.Constants;
using onlysats.domain.Entity;
using onlysats.domain.Enums;
using onlysats.domain.Events;
using onlysats.domain.Services.Repositories;
using onlysats.domain.Services.Request.Onboarding;
using onlysats.domain.Services.Response;
using onlysats.domain.Services.Response.Onboarding;
using System;

namespace onlysats.domain.Services
{

    /// <summary>
    /// Handles all account and settings setup (except wallet import).
    /// While wallet/BTCPay Server setup could be considered part of 
    /// onboarding, it's purposely encapsulated within the Accounting 
    /// service for extensibility without tightly coupling user
    /// setup with payment setup.
    /// </summary>
    public interface IOnboardingService
    {
        /// <summary>
        /// Sets up a new Creator with the supplied personal information with default settings
        /// </summary>
        Task<SetupCreatorResponse> SetupCreator(SetupCreatorRequest request);

        /// <summary>
        /// Sets up a new Patron with the supplied personal information with default settings
        /// </summary>
        Task<SetupPatronResponse> SetupPatron(SetupPatronRequest request);

        /// <summary>
        /// Updates a Creator's Settings/Preferences 
        /// </summary>
        Task<UpdateCreatorSettingsResponse> UpdateCreatorSettings(UpdateCreatorSettingsRequest request);

        /// <summary>
        /// Updates a Patron's Settings/Preferences
        /// </summary>
        Task<UpdatePatronSettingsResponse> UpdatePatronSettings(UpdatePatronSettingsRequest request);

        /// <summary>
        /// Loads the details of a Creator
        /// </summary>
        Task<LoadCreatorProfileResponse> LoadCreatorProfile(LoadCreatorProfileRequest request);

        /// <summary>
        /// Loads the details of a Patron
        /// </summary>
        Task<LoadPatronProfileResponse> LoadPatronProfile(LoadPatronProfileRequest request);
    }

    #region Implementation

    public class OnboardingService : IOnboardingService
    {
        private readonly IUserAccountRepository _UserAccountRepository;
        private readonly ICreatorRepository _CreatorRepository;
        private readonly IPatronRepository _PatronRepository;
        private readonly IChatService _ChatService;
        private readonly IMessagePublisher _MessagePublisher;

        public OnboardingService(IUserAccountRepository userAccountRepository,
                                ICreatorRepository creatorRepository,
                                IPatronRepository patronRepository,
                                IChatService chatService,
                                IMessagePublisher messagePublisher
                            )
        {
            _UserAccountRepository = userAccountRepository;
            _CreatorRepository = creatorRepository;
            _PatronRepository = patronRepository;
            _ChatService = chatService;
            _MessagePublisher = messagePublisher;
        }

        public async Task<LoadCreatorProfileResponse> LoadCreatorProfile(LoadCreatorProfileRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new LoadCreatorProfileResponse()
                            .BadRequest(CErrorMessage.LOAD_CREATOR_BAD_REQUEST);
            }

            var creatorDetails = await _CreatorRepository.GetCreatorDetail(request.CreatorId);

            if (creatorDetails == null)
            {
                return new LoadCreatorProfileResponse().NotFound();
            }

            return new LoadCreatorProfileResponse
            {
                Creator = creatorDetails
            }.OK();
        }

        public async Task<LoadPatronProfileResponse> LoadPatronProfile(LoadPatronProfileRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new LoadPatronProfileResponse()
                            .BadRequest(CErrorMessage.LOAD_PATRON_BAD_REQUEST);
            }

            var patronDetails = await _PatronRepository.GetPatronDetail(request.PatronId);

            if (patronDetails == null)
            {
                return new LoadPatronProfileResponse().NotFound();
            }

            return new LoadPatronProfileResponse
            {
                Patron = patronDetails
            }.OK();
        }

        public async Task<SetupCreatorResponse> SetupCreator(SetupCreatorRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new SetupCreatorResponse()
                            .BadRequest(CErrorMessage.SETUP_CREATOR_BAD_REQUEST);
            }

            var userAccount = await _UserAccountRepository.UpsertUserAccount(new UserAccount
            {
                Username = request.Username,
                Email = request.Email,
                Role = EUserRole.CREATOR,
                PasswordHash = HashService.HashSHA256(request.Password)
            });

            if (userAccount == null)
            {
                return new SetupCreatorResponse()
                            .ServerError(CErrorMessage.SETUP_CREATOR_FAIL_USER_ACCOUNT);
            }

            var creator = await _CreatorRepository.UpsertCreator(new Creator
            {
                UserAccountId = userAccount.Id
            });

            if (creator == null)
            {
                return new SetupCreatorResponse()
                            .ServerError(CErrorMessage.SETUP_CREATOR_FAIL);
            }

            // TODO: create synapse user

            var domainEvent = new NewCreatorJoinedEvent
            {
                UserAccountId = userAccount.Id,
                CreatorId = creator.Id,
                Username = userAccount.Username
            };

            await _MessagePublisher.PublishEvent(domainEvent.Topic, domainEvent);

            return new SetupCreatorResponse
            {
                UserAccountId = userAccount.Id,
                CreatorId = creator.Id
            }.OK();
        }

        public async Task<SetupPatronResponse> SetupPatron(SetupPatronRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new SetupPatronResponse()
                            .BadRequest(CErrorMessage.SETUP_PATRON_BAD_REQUEST);
            }

            var userAccount = await _UserAccountRepository.UpsertUserAccount(new UserAccount
            {
                Username = request.Username,
                Email = request.Email,
                Role = EUserRole.PATRON,
                PasswordHash = HashService.HashSHA256(request.Password)
            });

            if (userAccount == null)
            {
                return new SetupPatronResponse()
                            .ServerError(CErrorMessage.SETUP_PATRON_FAIL_USER_ACCOUNT);
            }

            var patron = await _PatronRepository.UpsertPatron(new Patron
            {
                UserAccountId = userAccount.Id,
                MemberUntil = request.MemberUntil
            });

            if (patron == null)
            {
                return new SetupPatronResponse()
                            .ServerError(CErrorMessage.SETUP_PATRON_FAIL);
            }


            // TODO: create synapse user

            var domainEvent = new NewPatronJoinedEvent
            {
                PatronId = patron.Id,
            };

            await _MessagePublisher.PublishEvent(domainEvent.Topic, domainEvent);

            return new SetupPatronResponse
            {
                PatronId = patron.Id,
                UserAccountId = userAccount.Id
            }.OK();
        }

        public async Task<UpdateCreatorSettingsResponse> UpdateCreatorSettings(UpdateCreatorSettingsRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new UpdateCreatorSettingsResponse()
                            .BadRequest(CErrorMessage.UPDATE_CREATOR_SETTINGS_BAD_REQUEST);
            }

            bool updatedChatSettings = false;
            if (request.ChatHideOutgoingMassMessages != null ||
                request.ChatShowWelcomeMessage != null)
            {
                var chatSettings = await _CreatorRepository.GetCreatorChatSettings(request.CreatorId);

                if (chatSettings == null)
                {
                    return new UpdateCreatorSettingsResponse()
                                .ServerError(CErrorMessage.UPDATE_CREATOR_SETTINGS_FAIL);
                }

                if (request.ChatHideOutgoingMassMessages != null)
                {
                    chatSettings.HideOutgoingMassMessages = request.ChatHideOutgoingMassMessages.Value;
                }

                if (request.ChatShowWelcomeMessage != null)
                {
                    chatSettings.ShowWelcomeMessage = request.ChatShowWelcomeMessage.Value;
                }

                chatSettings = await _CreatorRepository.UpsertCreatorChatSettings(chatSettings);

                updatedChatSettings = chatSettings != null;
            }

            bool updatedNotificationSettings = false;
            if (request.NotificationUsePush != null ||
                request.NotificationUseEmail != null ||
                request.NotificationNewMessage != null ||
                request.NotificationNewSubscriber != null ||
                request.NotificationNewTip != null ||
                request.NotificationNewPurchase != null)
            {
                var notificationSettings = await _CreatorRepository.GetCreatorNotificationSettings(request.CreatorId);

                if (notificationSettings == null)
                {
                    return new UpdateCreatorSettingsResponse()
                                .ServerError(CErrorMessage.UPDATE_CREATOR_SETTINGS_FAIL);
                }

                if (request.NotificationUsePush != null)
                {
                    notificationSettings.UsePush = request.NotificationUsePush.Value;
                }

                if (request.NotificationUseEmail != null)
                {
                    notificationSettings.UseEmail = request.NotificationUseEmail.Value;
                }

                if (request.NotificationNewMessage != null)
                {
                    notificationSettings.NewMessage = request.NotificationNewMessage.Value;
                }

                if (request.NotificationNewSubscriber != null)
                {
                    notificationSettings.NewSubscriber = request.NotificationNewSubscriber.Value;
                }

                if (request.NotificationNewTip != null)
                {
                    notificationSettings.NewTip = request.NotificationNewTip.Value;
                }

                if (request.NotificationNewPurchase != null)
                {
                    notificationSettings.NewPurchase = request.NotificationNewPurchase.Value;
                }

                notificationSettings = await _CreatorRepository.UpsertCreatorNotificationSettings(notificationSettings);
                updatedNotificationSettings = notificationSettings != null;
            }

            bool updatedProfileSettings = false;
            if (request.DisplayName != null ||
                request.CoverPhotoUri != null ||
                request.ProfilePhotoUri != null ||
                request.SubscriptionPricePerMonth != null ||
                request.AboutHtml != null ||
                request.AmazonWishList != null)
            {
                var profileSettings = await _CreatorRepository.GetCreatorProfileSettings(request.CreatorId);

                if (profileSettings == null)
                {
                    return new UpdateCreatorSettingsResponse()
                                .ServerError(CErrorMessage.UPDATE_CREATOR_SETTINGS_FAIL);
                }

                if (request.DisplayName != null)
                {
                    profileSettings.DisplayName = request.DisplayName.Value;
                }

                if (request.CoverPhotoUri != null)
                {
                    profileSettings.CoverPhotoUri = request.CoverPhotoUri.Value;
                }

                if (request.ProfilePhotoUri != null)
                {
                    profileSettings.ProfilePhotoUri = request.ProfilePhotoUri.Value;
                }

                if (request.SubscriptionPricePerMonth != null)
                {
                    profileSettings.SubscriptionPricePerMonth = request.SubscriptionPricePerMonth.Value;
                }

                if (request.AboutHtml != null)
                {
                    profileSettings.AboutHtml = request.AboutHtml.Value;
                }

                if (request.AmazonWishList != null)
                {
                    profileSettings.AmazonWishList = request.AmazonWishList.Value;
                }

                profileSettings = await _CreatorRepository.UpsertCreatorProfileSettings(profileSettings);
                updatedProfileSettings = profileSettings != null;
            }

            bool updatedSecuritySettings = false;
            if (request.ShowActivityStatus != null ||
                request.FullyPrivateProfile != null ||
                request.ShowPatronCount != null ||
                request.ShowMediaCount != null ||
                request.WatermarkPhotos != null ||
                request.WatermarkVideos != null)
            {
                var securitySettings = await _CreatorRepository.GetCreatorSecuritySettings(request.CreatorId);

                if (securitySettings == null)
                {
                    return new UpdateCreatorSettingsResponse()
                                .ServerError(CErrorMessage.UPDATE_CREATOR_SETTINGS_FAIL);
                }

                if (request.ShowActivityStatus != null)
                {
                    securitySettings.ShowActivityStatus = request.ShowActivityStatus.Value;
                }

                if (request.FullyPrivateProfile != null)
                {
                    securitySettings.FullyPrivateProfile = request.FullyPrivateProfile.Value;
                }

                if (request.ShowPatronCount != null)
                {
                    securitySettings.ShowPatronCount = request.ShowPatronCount.Value;
                }

                if (request.ShowMediaCount != null)
                {
                    securitySettings.ShowMediaCount = request.ShowMediaCount.Value;
                }

                if (request.WatermarkPhotos != null)
                {
                    securitySettings.WatermarkPhotos = request.WatermarkPhotos.Value;
                }

                if (request.WatermarkVideos != null)
                {
                    securitySettings.WatermarkVideos = request.WatermarkVideos.Value;
                }

                securitySettings = await _CreatorRepository.UpsertCreatorSecuritySettings(securitySettings);
                updatedSecuritySettings = securitySettings != null;
            }

            return new UpdateCreatorSettingsResponse
            {
                UpdatedChatSettings = updatedChatSettings,
                UpdatedNotificationSettings = updatedNotificationSettings,
                UpdatedProfileSettings = updatedProfileSettings,
                UpdatedSecuritySettings = updatedSecuritySettings
            }.OK();
        }

        public async Task<UpdatePatronSettingsResponse> UpdatePatronSettings(UpdatePatronSettingsRequest request)
        {
            if (request == null || !request.IsValid())
            {
                return new UpdatePatronSettingsResponse()
                            .BadRequest(CErrorMessage.UPDATE_PATRON_SETTINGS_BAD_REQUEST);
            }

            throw new NotImplementedException();
        }
    }

    #endregion
}