namespace Application.Common.Interfaces.Authentication;

public interface IIdentityService
{
    Task<bool> RegisterAsync(Guid domainUserId, string email, string password, bool admin = false);
    Task<bool> UsersAnyAsync();
    Task<bool> RolesAnyAsync();
    Task<bool> CreateRoleAsync(string role);
    Task<IList<string>> GetRolesByUserAsync(string email);
}