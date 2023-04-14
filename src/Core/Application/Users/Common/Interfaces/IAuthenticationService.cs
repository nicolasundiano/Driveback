using Domain.Users.ValueObjects;

namespace Application.Users.Common.Interfaces;

public interface IAuthenticationService
{
    Task<bool> RegisterAsync(UserId domainUserId, string email, string password, string role);
    Task<bool> LoginAsync(string email, string password);
}