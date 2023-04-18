using System.Security.Claims;
using Application.Users.Authentication.Common.Interfaces;
using Application.Users.Authentication.Common.Models;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly ClaimsPrincipal _user;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext?.User ?? throw new Exception();
    }

    public bool IsAuthenticated() =>
        _user.Identity?.IsAuthenticated is true;

    public CurrentUserResponse GetCurrentUser()
    {
        if (IsAuthenticated() is false)
        {
            throw new Exception();
        }

        var id = _user.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var email = _user.FindFirstValue(ClaimTypes.Email)!;
        var firstName = _user.FindFirstValue(ClaimTypes.Name)!;
        var lastName = _user.FindFirstValue(ClaimTypes.Surname)!;
        var phone = _user.FindFirstValue(ClaimTypes.MobilePhone)!;

        return new CurrentUserResponse(id, email, firstName, lastName, phone);
    }
}