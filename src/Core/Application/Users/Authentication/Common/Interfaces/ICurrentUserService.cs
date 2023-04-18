using Application.Users.Authentication.Common.Models;

namespace Application.Users.Authentication.Common.Interfaces;

public interface ICurrentUserService
{
    bool IsAuthenticated();
    CurrentUserResponse GetCurrentUser();
}