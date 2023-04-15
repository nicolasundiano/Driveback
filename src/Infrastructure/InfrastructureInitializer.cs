using Infrastructure.Authentication.Identity;
using Infrastructure.Persistence;

namespace Infrastructure;

public static class InfrastructureInitializer
{
    public static async Task InitializeInfrastructure(this IServiceProvider serviceProvider)
    {
        await serviceProvider.InitializeAdminUser();
    }

    private static async Task InitializeAdminUser(this IServiceProvider serviceProvider)
    {
        await serviceProvider.InitializeIdentityRoles();
        await serviceProvider.InitializeDomainAdminUser();
        await serviceProvider.InitializeIdentityAdminUser();
    }
}