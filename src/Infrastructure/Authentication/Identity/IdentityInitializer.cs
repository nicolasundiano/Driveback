using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Users;
using Infrastructure.Authentication.Common.Models;
using Infrastructure.Authentication.Identity.Initialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication.Identity;

public static class IdentityInitializer
{
    internal static async Task InitializeIdentity(this IServiceProvider serviceProvider)
    {
        await serviceProvider.InitializeIdentityRoles();
        await serviceProvider.InitializeIdentityAdminUser();
    }
    

    private static async Task InitializeIdentityRoles(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        
        var authenticationRepository = scope.ServiceProvider.GetRequiredService<IIdentityService>();
        
        await IdentityDbInitializer.InitializeIdentityRoles(authenticationRepository);
    }

    private static async Task InitializeIdentityAdminUser(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var adminUserSettings = scope.ServiceProvider.GetRequiredService<IOptions<AdminUserSettings>>();

        var authenticationRepository = scope.ServiceProvider.GetRequiredService<IIdentityService>();

        var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();

        await IdentityDbInitializer.InitializeIdentityAdminUser(
            adminUserSettings.Value,
            authenticationRepository,
            userRepository);
    }
}