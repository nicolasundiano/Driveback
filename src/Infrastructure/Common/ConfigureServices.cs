using Application.Common.Interfaces.Services;
using Infrastructure.Common.Models;
using Infrastructure.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Common;

internal static class ConfigureServices
{
    internal static IServiceCollection ConfigureCommonInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        services.ConfigureAdminUser(configuration);

        return services;
    }
    
    private static IServiceCollection ConfigureAdminUser(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var adminUserSettings = new AdminUserSettings();
        configuration.Bind(AdminUserSettings.SectionName, adminUserSettings);
        services.AddSingleton(Options.Create(adminUserSettings));
        return services;
    }
}