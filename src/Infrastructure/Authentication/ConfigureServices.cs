using Application.Users.Authentication.Common.Interfaces;
using Infrastructure.Authentication.Common.Models;
using Infrastructure.Authentication.Identity;
using Infrastructure.Authentication.Jwt;
using Infrastructure.Authentication.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication;

internal static class ConfigureServices
{
    internal static IServiceCollection ConfigureAuthentication(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddHttpContextAccessor();
        
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        services.ConfigureAdminUser(configuration);
        
        return services
            .ConfigureIdentity(configuration)
            .ConfigureJwt(configuration);
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