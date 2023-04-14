using Infrastructure.Identity;
using Infrastructure.Jwt;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .ConfigurePersistence(configuration)
            .ConfigureIdentity(configuration)
            .ConfigureJwt(configuration);
        
        return services;
    }
}