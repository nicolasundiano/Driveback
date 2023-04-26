using Application.Admins.Common.Specifications;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Users.Common.Specifications;
using Domain.Admins;
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
        IReadRepository<Admin> adminRepository)
    {
        if (!await authenticationService.UsersAnyAsync())
        {
            if (await adminRepository.GetAsync(new AdminSpecification(adminUserSettings.Email)) is { } admin)
            {
                await authenticationService.RegisterAsync(
                    admin.Id,
                    adminUserSettings.Email,
                    adminUserSettings.Password,
                    admin: true);
            }
        }
    }
}

