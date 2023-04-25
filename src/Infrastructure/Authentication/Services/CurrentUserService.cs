using System.Security.Claims;
using Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly ClaimsPrincipal _user;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext?.User ?? throw new Exception();
    }

    public bool IsAuthenticated =>
        _user.Identity?.IsAuthenticated is true;

    public Guid UserId => IsAuthenticated ?
        Guid.Parse(_user.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString()) : Guid.Empty;
}