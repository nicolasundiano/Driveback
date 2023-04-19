namespace Application.Users.Authentication.Common.Interfaces;

public interface IIdentityService
{
    Task<bool> RegisterAsync(Guid domainUserId, string email, string password);
    Task<bool> LoginAsync(string email, string password);
    Task<bool> UsersAnyAsync();
    Task<bool> RolesAnyAsync();
    Task<bool> CreateRoleAsync(string role);
    Task<IList<string>> GetRolesByUserAsync(string email);
}