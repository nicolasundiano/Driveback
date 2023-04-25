using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Users.Common.Specifications;
using Domain.Users;
using Infrastructure.Authentication.Common.Constants;
using Infrastructure.Authentication.Common.Models;

namespace Infrastructure.Authentication.Identity.Initialization;

public static class IdentityDbInitializer
{
    internal static async Task InitializeIdentityRoles(IIdentityService authenticationService)
    {
        if (!await authenticationService.RolesAnyAsync())
        {
            await authenticationService.CreateRoleAsync(UserRoles.Admin);
            await authenticationService.CreateRoleAsync(UserRoles.Basic);
        }
    }

    internal static async Task InitializeIdentityAdminUser(
        AdminUserSettings adminUserSettings,
        IIdentityService authenticationService,
        IRepository<User> userRepository)
    {
        if (!await authenticationService.UsersAnyAsync())
        {
            if (await userRepository.GetAsync(new UserSpecification(adminUserSettings.Email)) is { } user)
            {
                await authenticationService.RegisterAsync(
                    user.Id,
                    adminUserSettings.Email,
                    adminUserSettings.Password);
            }
        }
    }
}

