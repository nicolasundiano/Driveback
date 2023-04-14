namespace Application.Users.Common.Interfaces;

public interface IAuthenticationService
{
    Task<bool> RegisterAsync(string email, string password, string role);
    Task<bool> LoginAsync(string email, string password);
}