using Application.Users.Common.Interfaces;
using Domain.Users.ValueObjects;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<bool> RegisterAsync(UserId domainUserId, string email, string password, string role)
    {
        var identityUser = new ApplicationUser(domainUserId, email);

        var registerResult = await _userManager.CreateAsync(identityUser, password);
        
        if (registerResult.Succeeded)
        {
            var addToRoleResult = await _userManager.AddToRoleAsync(identityUser, role);

            if (addToRoleResult.Succeeded)
            {
                return true;
            }

            await _userManager.DeleteAsync(identityUser);
        }

        return false;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var loginResult = await _signInManager.PasswordSignInAsync(email, password, false, true);

        return loginResult.Succeeded;
    }
}