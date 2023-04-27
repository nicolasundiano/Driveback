using Application.Common.Interfaces.Authentication;
using Infrastructure.Authentication.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication.Identity.Services;

public class SignInService : ISignInService
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SignInService(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }
    public async Task<bool> SignInAsync(string email, string password)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(email, password, false, true);

        return signInResult.Succeeded;
    }
}