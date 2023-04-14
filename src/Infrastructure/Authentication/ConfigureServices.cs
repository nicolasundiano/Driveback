using Infrastructure.Authentication.Identity;
using Infrastructure.Authentication.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authentication;

internal static class ConfigureServices
{
    internal static IServiceCollection ConfigureAuthentication(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        return services
            .ConfigureIdentity(configuration)
            .ConfigureJwt(configuration);
    }
}