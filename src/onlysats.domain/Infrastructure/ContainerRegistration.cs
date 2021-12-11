using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using onlysats.domain.Models;
using onlysats.domain.Services;

namespace onlysats.domain.Infrastructure;

public static class ContainerRegistration
{
    public static void Register(IServiceCollection services, ConfigurationManager configuration)
    {
        var onlySatsConfig = new OnlySatsConfiguration
        {
            BtcPayUri = configuration["BtcPayServer:Uri"],
            BtcPayAdminUser = configuration["BtcPayServer:AdminUser"],
            BtcPayAdminPass = configuration["BtcPayServer:AdminPass"]
        };

        services.AddSingleton(onlySatsConfig);

        services.AddHttpClient<BtcPayServerProxy>(); // TODO: Add Polly Policies

        services.AddTransient<IAccountingService, AccountingService>();
        services.AddTransient<IChatService, ChatService>();
        services.AddTransient<IContentManagementService, ContentManagementService>();
        services.AddTransient<IFeedService, FeedService>();
        services.AddTransient<IFinderService, FinderService>();
        services.AddTransient<INotificationService, NotificationService>();
        services.AddTransient<IOnboardingService, OnboardingService>();
        services.AddTransient<IReportingService, ReportingService>();
    }
}
