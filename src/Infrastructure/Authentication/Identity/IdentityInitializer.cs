using Application.Common.Interfaces.Persistence;
using Application.Users.Authentication.Common.Interfaces;
using Domain.Users;
using Infrastructure.Authentication.Identity.Initialization;
using Infrastructure.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication.Identity;

public static class IdentityInitializer
{
    internal static async Task InitializeIdentityRoles(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        
        var authenticationRepository = scope.ServiceProvider.GetRequiredService<IIdentityService>();
        
        await IdentityDbInitializer.InitializeIdentityRoles(authenticationRepository);
    }

    internal static async Task InitializeIdentityAdminUser(this IServiceProvider serviceProvider)
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