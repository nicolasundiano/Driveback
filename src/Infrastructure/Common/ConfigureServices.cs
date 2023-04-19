using Application.Common.Interfaces.Services;
using Infrastructure.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common;

internal static class ConfigureServices
{
    internal static IServiceCollection ConfigureCommonInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}