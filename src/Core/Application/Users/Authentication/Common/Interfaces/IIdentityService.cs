using Domain.Users.ValueObjects;

namespace Application.Users.Authentication.Common.Interfaces;

public interface IIdentityService
{
    Task<bool> RegisterAsync(UserId domainUserId, string email, string password, string role);
    Task<bool> LoginAsync(string email, string password);
}