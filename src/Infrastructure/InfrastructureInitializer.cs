using Infrastructure.Authentication.Identity;

namespace Infrastructure;

public static class InfrastructureInitializer
{
    public static async Task InitializeInfrastructure(this IServiceProvider serviceProvider)
    {
        await serviceProvider.InitializeIdentity();
    }
}