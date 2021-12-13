using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using onlysats.domain.Models;
using onlysats.domain.Services;
using onlysats.domain.Services.Repositories;

namespace onlysats.domain.Infrastructure;

public static class ContainerRegistration
{
    public static void Register(IServiceCollection services, ConfigurationManager configuration)
    {
        var onlySatsConfig = new OnlySatsConfiguration
        {
            SqlConnectionString = configuration["Sql:ConnectionString"],
            BtcPayUri = configuration["BtcPayServer:Uri"],
            BtcPayAdminUser = configuration["BtcPayServer:AdminUser"],
            BtcPayAdminPass = configuration["BtcPayServer:AdminPass"]
        };

        services.AddSingleton(onlySatsConfig);

        services.AddHttpClient<BtcPayServerProxy>(); // TODO: Add Polly Policies

        services.AddScoped<ISqlRepository, SqlRepository>();
        services.AddScoped<IBlobRepository, BlobRepository>();

        services.AddTransient<IAssetRepository, AssetRepository>();
        services.AddTransient<ICreatorRepository, CreatorRepository>();
        services.AddTransient<IFeedRepository, FeedRepository>();
        services.AddTransient<IPatronRepository, PatronRepository>();
        services.AddTransient<IUserAccountRepository, UserAccountRepository>();
        services.AddTransient<IVaultRepository, VaultRepository>();
        services.AddTransient<INotificationRepository, NotificationRepository>();

        services.AddTransient<IAccountingService, AccountingService>();
        services.AddTransient<IChatService, ChatService>();
        services.AddTransient<IContentManagementService, ContentManagementService>();
        services.AddTransient<IFeedService, FeedService>();
        services.AddTransient<IFinderService, FinderService>();
        services.AddTransient<IUserEngagementService, UserEngagementService>();
        services.AddTransient<IOnboardingService, OnboardingService>();
        services.AddTransient<IReportingService, ReportingService>();
    }
}
