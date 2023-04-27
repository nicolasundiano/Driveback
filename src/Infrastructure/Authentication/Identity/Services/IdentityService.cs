using Application.Common.Interfaces.Authentication;
using Infrastructure.Authentication.Common.Constants;
using Infrastructure.Authentication.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<bool> RegisterAsync(Guid domainUserId, string email, string password, bool admin = false)
    {
        var identityUser = new ApplicationUser(domainUserId, email);

        var registerResult = await _userManager.CreateAsync(identityUser, password);
        
        if (registerResult.Succeeded)
        {
            var addToRoleResult = await _userManager.AddToRoleAsync(identityUser, admin? UserRoles.Admin : UserRoles.User);

            if (addToRoleResult.Succeeded)
            {
                return true;
            }

            await _userManager.DeleteAsync(identityUser);
        }

        return false;
    }

    public async Task<bool> UsersAnyAsync()
    {
        return await _userManager.Users.AnyAsync();
    }

    public async Task<bool> RolesAnyAsync()
    {
        return await _roleManager.Roles.AnyAsync();
    }

    public async Task<bool> CreateRoleAsync(string role)
    {
        var createRoleResult = await _roleManager.CreateAsync(new IdentityRole(role));

        return createRoleResult.Succeeded;
    }

    public async Task<IList<string>> GetRolesByUserAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            throw new Exception("user not found");
        }
        
        return await _userManager.GetRolesAsync(user);
    }
}