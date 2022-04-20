using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using onlysats.domain.Models;
using onlysats.domain.Services;
using onlysats.domain.Services.Repositories;

namespace onlysats.domain.Infrastructure
{
    public static class ContainerRegistration
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            var onlySatsConfig = new OnlySatsConfiguration
            {
                SqlConnectionString = configuration["Sql:ConnectionString"],
                BtcPayUri = configuration["BtcPayServer:Uri"],
                BtcPayAdminUser = configuration["BtcPayServer:AdminUser"],
                BtcPayAdminPass = configuration["BtcPayServer:AdminPass"],
                PubSubName = configuration["PubSub:Name"],
                SynapseUri = configuration["Synapse:Endpoint"],
                SynapseAdminUser = configuration["Synapse:AdminUser"],
                SynapseAdminPassword = configuration["Synapse:AdminPass"]
            };

            services.AddSingleton(onlySatsConfig);

            services.AddHttpClient<IBitcoinPaymentProcessor, BtcPayServerProxy>(); // TODO: Add Polly Policies
            services.AddHttpClient<IChatService, SynapseChatService>(); // TODO: Add Polly Policies

            services.AddScoped<IMessagePublisher, MessagePublisherProxy>();
            services.AddScoped<ISqlRepository, SqlRepository>();
            services.AddScoped<IBlobRepository, BlobRepository>();

            services.AddTransient<IAssetRepository, AssetRepository>();
            services.AddTransient<ICreatorRepository, CreatorRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IFeedRepository, FeedRepository>();
            services.AddTransient<IPatronRepository, PatronRepository>();
            services.AddTransient<IUserAccountRepository, UserAccountRepository>();
            services.AddTransient<IVaultRepository, VaultRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IPromotionRepository, PromotionRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();

            services.AddTransient<IAccountingService, AccountingService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IContentManagementService, ContentManagementService>();
            services.AddTransient<IFeedService, FeedService>();
            services.AddTransient<IFinderService, FinderService>();
            services.AddTransient<IUserEngagementService, UserEngagementService>();
            services.AddTransient<IOnboardingService, OnboardingService>();
            services.AddTransient<IReportingService, ReportingService>();
        }
    }
}