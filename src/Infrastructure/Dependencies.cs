using Infrastructure.Authentication;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Dependencies
{
    public static IServiceCollection ConfigureInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        return services
            .ConfigureCommonInfrastructure(configuration)
            .ConfigurePersistence(configuration)
            .ConfigureAuthentication(configuration);
    }
}