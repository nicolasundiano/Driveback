using Application.Users.Authentication.Common.Interfaces;
using Infrastructure.Authentication.Identity;
using Infrastructure.Authentication.Jwt;
using Infrastructure.Authentication.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authentication;

internal static class ConfigureServices
{
    internal static IServiceCollection ConfigureAuthentication(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddHttpContextAccessor();
        
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        return services
            .ConfigureIdentity(configuration)
            .ConfigureJwt(configuration);
    }
}